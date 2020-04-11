using System.Collections.Generic;
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
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
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
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "ironhasura.api" }
                }
            };
        }
    }
}