using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IronHasura.Configurations
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthorization(o => {
                o.AddPolicy("Administrator", policy => policy.RequireClaim("role", "Admin"));
            });

            return services;
        }
    }
}