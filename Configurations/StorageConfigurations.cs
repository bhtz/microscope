using IronHasura.Services;
using IronHasura.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IronHasura.Configurations
{
    public static class StorageConfiguration
    {
        public static IServiceCollection AddStorageConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var adapter = configuration.GetValue<string>("MCSP_FILE_ADAPTER");

            switch (adapter)
            {
                case "filesystem":
                    services.AddScoped<IStorageService, FileSystemStorageService>();
                    break;

                case "blobstorage":
                    services.AddScoped<IStorageService, BlobStorageService>();
                    break;

                case "aws":
                    services.AddScoped<IStorageService, AwsStorageService>();
                    break;

                default:
                    services.AddScoped<IStorageService, FileSystemStorageService>();
                    break;
            }

            return services;
        }
    }
}