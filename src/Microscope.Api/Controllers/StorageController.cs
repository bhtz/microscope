using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microscope.Features.Storage.Commands;
using Microscope.Features.Storage.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microscope.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Save blob in container
        /// </summary>
        /// <param name="file"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{containerName}")]
        public async Task<IActionResult> SaveBlob([FromForm] IFormFile file, [FromRoute] string containerName)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    
                    var cmd = new AddBlobCommand()
                    {
                        BlobName = file.FileName,
                        ContainerName = containerName,
                        Data = ms
                    };

                    await this._mediator.Send(cmd);

                    return Ok();
                }
            }
            else
            {
                return BadRequest("Bad request : empty file");
            }
        }

        /// <summary>
        /// Download blob from container
        /// </summary>
        /// <param name="containerName">container name</param>
        /// <param name="blobName">blob name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{containerName}/{blobName}")]
        public async Task<FileContentResult> GetBlob([FromRoute] string containerName, [FromRoute] string blobName)
        {
            var query = new GetBlobDataQuery() 
            {
                BlobName = blobName,
                ContainerName = containerName
            };
            
            var result = await this._mediator.Send(query);
            result.Data.Position = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                result.Data.CopyTo(ms);
                return File(ms.ToArray(), "application/octet-stream", blobName);
            }
        }

        /// <summary>
        /// List blobs in container
        /// </summary>
        /// <param name="containerName">container name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{containerName}")]
        public async Task<IEnumerable<string>> ListBlob([FromRoute] string containerName)
        {
            var query = new GetBlobsByContainerQuery()
            {
                ContainerName = containerName
            };

            var results = await this._mediator.Send(query);

            return results.Select(x => x.Name);
        }

        [HttpGet]
        public async Task<IEnumerable<string>> ListContainers()
        {
            var query = new FilteredContainerQuery();
            var results = await this._mediator.Send(query);

            return results.Select(x => x.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateContainer([FromBody] AddContainerCommand command)
        {
            await this._mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Delete blob in container
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{containerName}/{blobName}")]
        public async Task<IActionResult> DeleteBlob([FromRoute] string containerName, [FromRoute] string blobName)
        {
            try
            {
                var cmd = new DeleteBlobCommand()
                {
                    ContainerName = containerName,
                    BlobName = blobName
                };

                await this._mediator.Send(cmd);
                
                return Ok();   
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}