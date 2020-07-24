using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IronHasura.Configurations
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var authorityEndpoint = configuration.GetValue<string>("MCSP_HOST");

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var builder = services.AddAuthentication();

            builder.AddCookie(o =>
            {
                o.Cookie.SameSite = SameSiteMode.None;
                o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            if (configuration.GetSection("ExternalProviders:OIDC").Exists())
            {
                builder.AddOpenIdConnect(options => configuration.Bind("ExternalProviders:OIDC", options));
            }

            if (configuration.GetSection("ExternalProviders:Google").Exists())
            {
                builder.AddGoogle(options => configuration.Bind("ExternalProviders:Google", options));
            }

            if (configuration.GetSection("ExternalProviders:Microsoft").Exists())
            {
                builder.AddMicrosoftAccount(options => configuration.Bind("ExternalProviders:Microsoft", options));
            }

            if (configuration.GetSection("ExternalProviders:Github").Exists())
            {
                builder.AddGitHub(options => configuration.Bind("ExternalProviders:Github", options));
            }

            if (configuration.GetSection("ExternalProviders:Twitter").Exists())
            {
                builder.AddTwitter(options => configuration.Bind("ExternalProviders:Twitter", options));
            }

            builder.AddJwtBearer(o =>
            {
                o.Authority = authorityEndpoint;
                o.Audience = "mcsp.api";
                o.RequireHttpsMetadata = false;

                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = false,
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role
                };
            });

            return services;
        }
    }
}