using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronHasura.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace IronHasura.Controllers
{
    [Route("api/storage")]
    [ApiController]
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
    }
}