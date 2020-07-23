using System;
using System.IO;
using System.Threading.Tasks;
using IronHasura.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace IronHasura.Controllers
{
    [OpenApiIgnore]
    [Authorize(Roles = "Admin")]
    [Route("/Storage")]
    public class StorageMvcController : Controller
    {
        private IStorageService _storageService;

        public StorageMvcController(IStorageService storageService)
        {
            this._storageService = storageService;
        }

        public IActionResult Index()
        {
            var data = this._storageService.GetFiles().Result;
            return View(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string filename)
        {
            await this._storageService.DeleteFile(filename);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/storage/{filename}/download")]
        public async Task<IActionResult> Download(string filename)
        {
            byte[] data = await this._storageService.GetFileData(filename);
            return File(data, "application/force-download", filename);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        [Route("/storage/Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length != 0)
            {
                Guid id = Guid.NewGuid();
                var ext = Path.GetExtension(file.FileName);
                var name = id + "_" + file.FileName;
                await this._storageService.SaveFile(name, file);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
