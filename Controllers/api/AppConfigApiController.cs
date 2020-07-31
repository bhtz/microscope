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
    [Route("api/remoteconfigs")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppConfigApiController : ControllerBase
    {
        private readonly IronHasuraDbContext _context;

        public AppConfigApiController(IronHasuraDbContext context)
        {
            _context = context;
        }

        // GET: api/AppConfigApiApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemoteConfig>>> GetAppConfig()
        {
            return await _context.RemoteConfig.ToListAsync();
        }

        // GET: api/AppConfigApiApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RemoteConfig>> GetAppConfig(Guid id)
        {
            var appConfig = await _context.RemoteConfig.FindAsync(id);

            if (appConfig == null)
            {
                return NotFound();
            }

            return appConfig;
        }

        // PUT: api/AppConfigApiApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppConfig(Guid id, RemoteConfig appConfig)
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
        public async Task<ActionResult<RemoteConfig>> PostAppConfig(RemoteConfig appConfig)
        {
            _context.RemoteConfig.Add(appConfig);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppConfig", new { id = appConfig.Id }, appConfig);
        }

        // DELETE: api/AppConfigApiApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RemoteConfig>> DeleteAppConfig(Guid id)
        {
            var appConfig = await _context.RemoteConfig.FindAsync(id);
            if (appConfig == null)
            {
                return NotFound();
            }

            _context.RemoteConfig.Remove(appConfig);
            await _context.SaveChangesAsync();

            return appConfig;
        }

        private bool AppConfigExists(Guid id)
        {
            return _context.RemoteConfig.Any(e => e.Id == id);
        }
    }
}
