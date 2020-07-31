using System;
using System.IO;
using System.Threading.Tasks;
using Microscope.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Microscope.Controllers
{
    [Route("api/storage")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StorageController : ControllerBase
    {
        private IStorageService _storageService { get; set; }

        public StorageController(IStorageService storageService)
        {
            this._storageService = storageService;
        }

        /// <summary>
        /// Upload file 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            Guid id = Guid.NewGuid();
            var ext = Path.GetExtension(file.FileName);
            var name = id + "_" + file.FileName;

            await this._storageService.SaveFile(name, file);

            return Ok(name);
        }

        /// <summary>
        /// Get file uri
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{filename}")]
        public async Task<IActionResult> GetFile([FromRoute] string filename)
        {
            string mimeType;

            byte[] data = await this._storageService.GetFileData(filename);

            var provider = new FileExtensionContentTypeProvider();
            bool isKnownType = provider.TryGetContentType(filename, out mimeType);

            if (!isKnownType)
            {
                mimeType = "application/octet-stream";
            }

            return new FileContentResult(data, mimeType);
        }

        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{filename}/download")]
        public async Task<IActionResult> Download([FromRoute] string filename)
        {
            byte[] data = await this._storageService.GetFileData(filename);
            return File(data, "application/force-download", filename);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            string[] data = await this._storageService.GetFiles();
            return Ok(data);
        }
    }
}