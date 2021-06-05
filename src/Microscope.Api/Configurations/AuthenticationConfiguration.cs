using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;

namespace Microscope.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.Authority = configuration["Jwt:Authority"];
                o.Audience = configuration["Jwt:Audience"];

                //Docker OK : TODO ValidIssuer for prod ???
             // o.TokenValidationParameters.ValidIssuer = configuration["Jwt:Authority"]
                o.TokenValidationParameters.ValidateIssuer = false;
                //END Docker OK

                o.TokenValidationParameters.ValidateAudience = false;
                o.RequireHttpsMetadata = false;

                //o.TokenValidationParameters.ValidAudiences = new string[] { "master-realm", "account", Configuration["Jwt:Audience"] };

                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();

                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";

                        var t = o.Authority;
                        var r = o.Audience;
                        Console.WriteLine("\n" + t + " : " + r + "\n");

                        return c.Response.WriteAsync(c.Exception.InnerException.Message);
                    }
                };
            });

            return services;
        }
    }
}