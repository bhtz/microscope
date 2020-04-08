using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IronHasura.Models;
using Microsoft.AspNetCore.Authorization;

namespace hasura_webhook_dotnet.Controllers_mvc
{
    [Authorize]
    public class AppConfigController : Controller
    {
        private readonly IronHasuraDbContext _context;

        public AppConfigController(IronHasuraDbContext context)
        {
            _context = context;
        }

        // GET: AppConfig
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppConfig.ToListAsync());
        }

        // GET: AppConfig/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appConfig = await _context.AppConfig
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appConfig == null)
            {
                return NotFound();
            }

            return View(appConfig);
        }

        // GET: AppConfig/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppConfig/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value")] AppConfig appConfig)
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

        // GET: AppConfig/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appConfig = await _context.AppConfig.FindAsync(id);
            if (appConfig == null)
            {
                return NotFound();
            }
            return View(appConfig);
        }

        // POST: AppConfig/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Value")] AppConfig appConfig)
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

        // GET: AppConfig/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appConfig = await _context.AppConfig
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appConfig == null)
            {
                return NotFound();
            }

            return View(appConfig);
        }

        // POST: AppConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appConfig = await _context.AppConfig.FindAsync(id);
            _context.AppConfig.Remove(appConfig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppConfigExists(Guid id)
        {
            return _context.AppConfig.Any(e => e.Id == id);
        }
    }
}
