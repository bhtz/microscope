using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IronHasura.Configurations
{
    public static class IdentityServerConfiguration
    {
        public static IServiceCollection AddIdentityServerConfiguration(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetResources())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients());

            return services;
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var roleRessource = new IdentityResource(name: "roles", displayName: "Roles", claimTypes: new[] { "role" });

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                roleRessource
            };
        }

        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ironhasura.api", "Iron hasura API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "AngularClient",
                    ClientName = "Angular Client",
                    ClientUri = "http://localhost:4200",
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200/" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AccessTokenLifetime = 3600,
                    AllowedScopes = { "openid", "profile", "email", "roles", "ironhasura.api" }
                }
            };
        }
    }
}