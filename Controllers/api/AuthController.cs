using System;
using System.Linq;
using IronHasura.Dto;
using IronHasura.Models;
using Microsoft.AspNetCore.Mvc;

namespace IronHasura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IronHasuraDbContext _context;

        public AuthController(IronHasuraDbContext context)
        {
            this._context = context;
        }

        /**
            GET HASURA CLAIMS from bearer token
         */
        [HttpGet]
        public ActionResult<HasuraClaims> Get()
        {
            var hasuraClaims = new HasuraClaims();
            Console.WriteLine("HASURA CONNECTED");

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirst("sub")?.Value;
                var role = string.IsNullOrEmpty(HttpContext.User.FindFirst("role")?.Value)
                    ? HttpContext.User.FindFirst("role")?.Value
                    : "user";

                if (Boolean.Parse(Environment.GetEnvironmentVariable("REFERENCE_USER")))
                {
                    this.SaveUser();
                }

                hasuraClaims.UserId = Guid.Parse(userId);
                hasuraClaims.Role = role;
            }
            else
            {
                hasuraClaims.Role = "anonymous";
            }

            return hasuraClaims;
        }

        private void SaveUser()
        {
            string sub = HttpContext.User.FindFirst("sub")?.Value;
            string firstname = HttpContext.User.FindFirst("given_name")?.Value;
            string lastname = HttpContext.User.FindFirst("family_name")?.Value;
            string email = HttpContext.User.FindFirst("email")?.Value;
            string role = HttpContext.User.FindFirst("role")?.Value;

            if (!this._context.User.Any(x => x.Id == Guid.Parse(sub)))
            {
                User user = new User();
                user.Id = Guid.Parse(sub);
                user.Firstname = firstname;
                user.Lastname = lastname;
                user.Email = email;
                user.Role = role;
                
                this._context.User.Add(user);
                this._context.SaveChanges();
            }
        }
    }
}
