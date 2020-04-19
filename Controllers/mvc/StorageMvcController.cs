using System.Threading.Tasks;
using IronHasura.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronHasura.Controllers
{
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

    }
}
