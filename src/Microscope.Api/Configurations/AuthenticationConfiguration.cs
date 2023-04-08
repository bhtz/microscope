using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microscope.Api.Services;
using Microscope.ExternalSystems.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Api.Configurations;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        var JwtEvent = new JwtBearerEvents()
        {
            OnAuthenticationFailed = c =>
            {
                c.NoResult();

                c.Response.StatusCode = 500;
                c.Response.ContentType = "text/plain";

                return c.Response.WriteAsync(c.Exception.InnerException.Message);
            }
        };

        var tenants = configuration.GetSection("Tenants").Get<List<JWTTenantConfiguration>>();

        foreach (var tenant in tenants)
        {
            authenticationBuilder.AddJwtBearer(o =>
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                
                o.Authority = tenant.Authority;
                o.Audience = tenant.Audience;
                
                if (!string.IsNullOrEmpty(tenant.RoleClaim))
                {
                    o.TokenValidationParameters.RoleClaimType = tenant.RoleClaim;
                }

                o.TokenValidationParameters.ValidateIssuer = false;
                o.TokenValidationParameters.ValidateAudience = false;
                
                o.RequireHttpsMetadata = false;
                o.Events = JwtEvent;
            });    
        }
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}

public class JWTTenantConfiguration
{
    public string Authority { get; set; }
    public string Audience { get; set; }
    public string RoleClaim { get; set; }
}
