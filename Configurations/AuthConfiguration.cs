using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IronHasura.Configurations
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var authorityEndpoint = configuration.GetValue<string>("IRONHASURA_AUTHORITY_ENDPOINT");
            var audience = configuration.GetValue<string>("IRONHASURA_AUDIENCE");

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
                .AddAuthentication()
                .AddCookie()
                .AddJwtBearer(o =>
                {
                    o.Authority = authorityEndpoint;
                    o.Audience = audience;
                    o.RequireHttpsMetadata = false;

                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                    };
                });

            return services;
        }
    }
}