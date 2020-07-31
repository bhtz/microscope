using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microscope.Data;
using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;

namespace Microscope.Controllers_mvc
{
    [OpenApiIgnore]
    [Authorize(Roles = "Admin")]
    public class RemoteConfigController : Controller
    {
        private readonly IronHasuraDbContext _context;

        public RemoteConfigController(IronHasuraDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.RemoteConfig.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appConfig = await _context.RemoteConfig.FirstOrDefaultAsync(m => m.Id == id);
            if (appConfig == null)
            {
                return NotFound();
            }

            return View(appConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,Value")] RemoteConfig appConfig)
        {
            if (ModelState.IsValid)
            {
                appConfig.Id = Guid.NewGuid();
                _context.Add(appConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appConfig = await _context.RemoteConfig.FindAsync(id);
            if (appConfig == null)
            {
                return NotFound();
            }
            return View(appConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Key,Value")] RemoteConfig appConfig)
        {
            if (id != appConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppConfigExists(appConfig.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appConfig = await _context.RemoteConfig
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appConfig == null)
            {
                return NotFound();
            }

            return View(appConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appConfig = await _context.RemoteConfig.FindAsync(id);
            _context.RemoteConfig.Remove(appConfig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool AppConfigExists(Guid id)
        {
            return _context.RemoteConfig.Any(e => e.Id == id);
        }
    }
}
