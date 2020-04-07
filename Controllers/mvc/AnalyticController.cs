using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IronHasura.Models;

namespace hasura_webhook_dotnet.Controllers_mvc
{
    public class AnalyticController : Controller
    {
        private readonly IronHasuraDbContext _context;

        public AnalyticController(IronHasuraDbContext context)
        {
            _context = context;
        }

        // GET: Analytic
        public async Task<IActionResult> Index()
        {
            return View(await _context.Analytic.ToListAsync());
        }

        // GET: Analytic/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analytic = await _context.Analytic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analytic == null)
            {
                return NotFound();
            }

            return View(analytic);
        }

        // GET: Analytic/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Analytic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,Dimension")] Analytic analytic)
        {
            if (ModelState.IsValid)
            {
                analytic.Id = Guid.NewGuid();
                _context.Add(analytic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(analytic);
        }

        // GET: Analytic/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analytic = await _context.Analytic.FindAsync(id);
            if (analytic == null)
            {
                return NotFound();
            }
            return View(analytic);
        }

        // POST: Analytic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Key,Dimension")] Analytic analytic)
        {
            if (id != analytic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analytic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalyticExists(analytic.Id))
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
            return View(analytic);
        }

        // GET: Analytic/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analytic = await _context.Analytic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analytic == null)
            {
                return NotFound();
            }

            return View(analytic);
        }

        // POST: Analytic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var analytic = await _context.Analytic.FindAsync(id);
            _context.Analytic.Remove(analytic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalyticExists(Guid id)
        {
            return _context.Analytic.Any(e => e.Id == id);
        }
    }
}
