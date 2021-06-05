using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microscope.Domain.Services;
using Microscope.Infrastructure.Storage;
using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Infrastructure.Repositories;
using Microscope.Domain.Aggregates.AnalyticAggregate;
using System;

namespace Microscope.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var brandName = "Microscope";
            var provider = configuration.GetValue<string>("DatabaseProvider");
            var connectionString = configuration.GetConnectionString(brandName);
            var assemblyName = typeof(MicroscopeDbContext).Assembly.FullName;

            services.AddDbContext<MicroscopeDbContext>(options =>
            {
                switch (provider)
                {
                    case "postgres":
                        options.UseNpgsql(connectionString, o => o.MigrationsAssembly(assemblyName));
                        break;

                    case "mssql":
                        options.UseSqlServer(connectionString, o => o.MigrationsAssembly(assemblyName));
                        break;

                    default:
                        options.UseInMemoryDatabase(brandName);
                        break;
                }
            });

            services.AddScoped<IRemoteConfigRepository, RemoteConfigRepository>();
            services.AddScoped<IAnalyticRepository, AnalyticRepository>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            StorageOptions options = new StorageOptions();
            IConfigurationSection section = configuration.GetSection("Storage");
            section.Bind(options);
            
            services.Configure<StorageOptions>(settings => section.Bind(settings));

            switch (options.Adapter)
            {
                case "filesystem":
                    services.AddScoped<IStorageService, FileSystemStorageService>();
                    break;

                case "azure":
                    services.AddScoped<IStorageService, BlobStorageService>();
                    break;

                case "aws":
                    services.AddScoped<IStorageService, AwsStorageService>();
                    break;

                case "minio":
                    services.AddScoped<IStorageService, MinioStorageService>();
                    break;

                default:
                    services.AddScoped<IStorageService, MinioStorageService>();
                    break;
            }

            return services;
        }
    }
}