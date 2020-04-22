using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronHasura.Data;
using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;

namespace IronHasura.Controllers_mvc
{
    [OpenApiIgnore]
    [Authorize(Roles = "Admin")]
    public class AnalyticController : Controller
    {
        private readonly IronHasuraDbContext _context;

        public AnalyticController(IronHasuraDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Analytic.ToListAsync());
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

            var analytic = await _context.Analytic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analytic == null)
            {
                return NotFound();
            }

            return View(analytic);
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

            var analytic = await _context.Analytic.FindAsync(id);
            if (analytic == null)
            {
                return NotFound();
            }
            return View(analytic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
