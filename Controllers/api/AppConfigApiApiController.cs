using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronHasura.Models;

namespace IronHasura.Controllers_api
{
    [Route("api/remoteconfigs")]
    [ApiController]
    public class AppConfigApiController : ControllerBase
    {
        private readonly IronHasuraDbContext _context;

        public AppConfigApiController(IronHasuraDbContext context)
        {
            _context = context;
        }

        // GET: api/AppConfigApiApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppConfig>>> GetAppConfig()
        {
            return await _context.AppConfig.ToListAsync();
        }

        // GET: api/AppConfigApiApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppConfig>> GetAppConfig(Guid id)
        {
            var appConfig = await _context.AppConfig.FindAsync(id);

            if (appConfig == null)
            {
                return NotFound();
            }

            return appConfig;
        }

        // PUT: api/AppConfigApiApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppConfig(Guid id, AppConfig appConfig)
        {
            if (id != appConfig.Id)
            {
                return BadRequest();
            }

            _context.Entry(appConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppConfigExists(id))
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

        // POST: api/AppConfigApiApi
        [HttpPost]
        public async Task<ActionResult<AppConfig>> PostAppConfig(AppConfig appConfig)
        {
            _context.AppConfig.Add(appConfig);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppConfig", new { id = appConfig.Id }, appConfig);
        }

        // DELETE: api/AppConfigApiApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppConfig>> DeleteAppConfig(Guid id)
        {
            var appConfig = await _context.AppConfig.FindAsync(id);
            if (appConfig == null)
            {
                return NotFound();
            }

            _context.AppConfig.Remove(appConfig);
            await _context.SaveChangesAsync();

            return appConfig;
        }

        private bool AppConfigExists(Guid id)
        {
            return _context.AppConfig.Any(e => e.Id == id);
        }
    }
}
