using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using IdentityServer4;
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

            services
                .AddAuthentication()
                .AddCookie(o => {
                    o.Cookie.SameSite = SameSiteMode.None;
                })
                .AddOpenIdConnect("aad", "Azure AD", options =>
                {
                    options.Authority = configuration.GetValue<string>("ExternalProviders:AAD:Authority");
                    options.ClientId = configuration.GetValue<string>("ExternalProviders:AAD:ClientId");
                    options.ClientSecret = configuration.GetValue<string>("ExternalProviders:AAD:ClientSecret");
                    options.RequireHttpsMetadata = true;
                    options.ResponseType = "code";
                    options.SaveTokens = true;
                })
                .AddJwtBearer(o =>
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