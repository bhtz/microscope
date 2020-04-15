using System;
using System.Linq;
using System.Threading.Tasks;
using IronHasura.Dto;
using IronHasura.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IronHasura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IronHasuraDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IronHasuraDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        /**
            GET HASURA CLAIMS from bearer token
         */
        [HttpGet]
        public ActionResult<HasuraClaims> Get()
        {
            var hasuraClaims = new HasuraClaims();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirst("sub")?.Value;
                var role = string.IsNullOrEmpty(HttpContext.User.FindFirst("role")?.Value)
                    ? "user"
                    : HttpContext.User.FindFirst("role")?.Value;

                // var user = await this._userManager.GetUserAsync(User);
                // var roles = await this._userManager.GetRolesAsync(user);

                hasuraClaims.UserId = Guid.Parse(userId);
                hasuraClaims.Role = role;
            }
            else
            {
                hasuraClaims.Role = "anonymous";
            }

            return hasuraClaims;
        }
    }
}
