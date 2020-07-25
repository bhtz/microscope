using System;
using IronHasura.Dto;
using IronHasura.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IronHasura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IronHasuraDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IronHasuraDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        /// <summary>
        /// GET claims from bearer token
        /// </summary>
        /// <returns>HASURA CLAIMS</returns>
        [HttpGet]
        public ActionResult<HasuraClaims> Get()
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
