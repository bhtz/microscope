using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microscope.Features.RemoteConfig.Commands;
using Microscope.Core.Queries.RemoteConfig;
using Microscope.Domain.Aggregates.RemoteConfig;
using Microscope.Features.RemoteConfig.Queries;

namespace Microscope.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RemoteConfigController : ControllerBase
    {
        private readonly IMediator _mediator;


        public RemoteConfigController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/RemoteConfig
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemoteConfig>>> GetRemoteConfigs()
        {
            var query = new FilteredRemoteConfigQuery();
            var results = await this._mediator.Send(query);

            return Ok(results);
        }

        // GET: api/RemoteConfig/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RemoteConfig>> GetRemoteConfig(Guid id)
        {
            var query = new GetRemoteConfigByIdQuery(id);
            var results = await this._mediator.Send(query);

            return Ok(results);
        }

        // PUT: api/RemoteConfig/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemoteConfig(Guid id, EditRemoteConfigCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await this._mediator.Send(command);

            return Ok();
        }

        // POST: api/RemoteConfig
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RemoteConfig>> PostRemoteConfig(AddRemoteConfigCommand command)
        {
            Guid idCreated = await this._mediator.Send(command);

            return CreatedAtAction("GetRemoteConfig", new { id = idCreated }, idCreated.ToString());
        }

        // DELETE: api/RemoteConfig/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemoteConfig(Guid id)
        {
            var cmd = new DeleteRemoteConfigCommand(){ Id = id };
            await this._mediator.Send(cmd);

            return NoContent();
        }

    }
}
