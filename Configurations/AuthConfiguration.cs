using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
                .AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.Cookie.SameSite = SameSiteMode.None;
                    o.Cookie.SecurePolicy = CookieSecurePolicy.None;
                })
                // .AddOpenIdConnect("aad", "Azure AD", options =>
                // {
                //     // options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //     options.Authority = configuration.GetValue<string>("ExternalProviders:AAD:Authority");
                //     options.ClientId = configuration.GetValue<string>("ExternalProviders:AAD:ClientId");
                //     options.ClientSecret = configuration.GetValue<string>("ExternalProviders:AAD:ClientSecret");
                //     options.SaveTokens = true;
                // })
                .AddAzureAD(options =>
                {
                    options.Instance = configuration.GetValue<string>("ExternalProviders:AAD:Instance");
                    options.TenantId = configuration.GetValue<string>("ExternalProviders:AAD:TenantId");
                    options.ClientId = configuration.GetValue<string>("ExternalProviders:AAD:ClientId");
                    options.Domain = "soprasteria.onmicrosoft.com";
                    options.ClientSecret = configuration.GetValue<string>("ExternalProviders:AAD:ClientSecret");
                })
                .AddGoogle(options =>
                {
                    //options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = configuration.GetValue<string>("ExternalProviders:Google:ClientId");
                    options.ClientSecret = configuration.GetValue<string>("ExternalProviders:Google:ClientSecret");
                    options.SaveTokens = true;
                })
                // .AddMicrosoftAccount(o => {
                //     o.ClientId = configuration.GetValue<string>("ExternalProviders:AAD:ClientId");
                //     o.ClientSecret = configuration.GetValue<string>("ExternalProviders:AAD:ClientSecret");
                // })
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

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";
                options.TokenValidationParameters.ValidateIssuer = false;
            });

            return services;
        }
    }
}