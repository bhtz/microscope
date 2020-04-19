using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace IronHasura.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory;

        public ProfileService(UserManager<IdentityUser> userManager, IUserClaimsPrincipalFactory<IdentityUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var user = this._userManager.GetUserAsync(context.Subject).Result;
                var roles = await this._userManager.GetRolesAsync(user);

                var principal = await this._claimsFactory.CreateAsync(user);
                if (principal == null) throw new Exception("ClaimsFactory failed to create a principal");

                var claims = principal.Claims.ToList();

                foreach (var item in context.RequestedClaimTypes)
                {
                    if (item == "https://hasura.io/jwt/claims")
                    {
                        var hasuraJwtClaimsData = new Dictionary<string, object>
                         {
                            { "x-hasura-allowed-roles", roles.Select(x => x.ToLower()) },
                            { "x-hasura-default-role", roles.FirstOrDefault().ToLower() },
                            { "x-hasura-user-id", user.Id },
                        };

                        var hasuraJwtClaims = new Claim(
                            "https://hasura.io/jwt/claims",
                            JsonConvert.SerializeObject(hasuraJwtClaimsData),
                            IdentityServerConstants.ClaimValueTypes.Json
                        );

                        claims.Add(hasuraJwtClaims);
                    }
                }

                claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
                context.AddRequestedClaims(claims);
            }
            catch (Exception)
            {
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;
            context.IsActive = user != null && user.LockoutEnd == null;

            return Task.FromResult(0);
        }
    }
}