using System;
using System.Threading.Tasks;
using IronHasura.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IronHasura.Controllers
{
    [Route("Storage")]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string filename)
        {
            await this._storageService.DeleteFile(filename);
            return RedirectToAction(nameof(Index));
        }

    }
}
