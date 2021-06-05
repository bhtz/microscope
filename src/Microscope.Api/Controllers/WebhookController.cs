using System;
using System.Security.Claims;
using Microscope.Hasura.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Microscope.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        [HttpGet]
        [Route("hasura")]
        public ActionResult<HasuraClaims> HasuraAuthHook()
        {
            var hasuraClaims = new HasuraClaims();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirstValue("sub") ??
                            HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var role =
                    string.IsNullOrEmpty(HttpContext.User.FindFirst("role")?.Value)
                    ? "user"
                    : HttpContext.User.FindFirst("role")?.Value;

                hasuraClaims.UserId = Guid.Parse(userId);
                hasuraClaims.Role = role.ToLower();
            }
            else
            {
                hasuraClaims.Role = "anonymous";
            }

            return hasuraClaims;
        }
    }
}
