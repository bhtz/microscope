using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microscope.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Microscope.Controllers_api
{
    [Route("api/analytics")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnalyticApiController : ControllerBase
    {
        private readonly IronHasuraDbContext _context;

        public AnalyticApiController(IronHasuraDbContext context)
        {
            _context = context;
        }

        // GET: api/AnalyticApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Analytic>>> GetAnalytic()
        {
            var isAdmin = User.IsInRole("Admin");
            var boolea = User.HasClaim("role", "Admin");
            var headers = this.Request.Headers;
            return await _context.Analytic.ToListAsync();
        }

        // GET: api/AnalyticApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Analytic>> GetAnalytic(Guid id)
        {
            var analytic = await _context.Analytic.FindAsync(id);

            if (analytic == null)
            {
                return NotFound();
            }

            return analytic;
        }

        // PUT: api/AnalyticApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalytic(Guid id, Analytic analytic)
        {
            if (id != analytic.Id)
            {
                return BadRequest();
            }

            _context.Entry(analytic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalyticExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AnalyticApi
        [HttpPost]
        public async Task<ActionResult<Analytic>> PostAnalytic(Analytic analytic)
        {
            _context.Analytic.Add(analytic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnalytic", new { id = analytic.Id }, analytic);
        }

        // DELETE: api/AnalyticApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Analytic>> DeleteAnalytic(Guid id)
        {
            var analytic = await _context.Analytic.FindAsync(id);
            if (analytic == null)
            {
                return NotFound();
            }

            _context.Analytic.Remove(analytic);
            await _context.SaveChangesAsync();

            return analytic;
        }

        private bool AnalyticExists(Guid id)
        {
            return _context.Analytic.Any(e => e.Id == id);
        }
    }
}
