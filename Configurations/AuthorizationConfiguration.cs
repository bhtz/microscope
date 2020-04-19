using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IronHasura.Configurations
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();

            return services;
        }
    }
}