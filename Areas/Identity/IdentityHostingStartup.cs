using System;
using Microscope.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Microscope.Areas.Identity.IdentityHostingStartup))]
namespace Microscope.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                var connectionString = 
                    context.Configuration.GetConnectionString("MCSP_IDENTITY_CS") ?? 
                    context.Configuration.GetConnectionString("MCSP_DATA_CS") ??
                    context.Configuration.GetValue<string>("MCSP_DATA_CS");

                services.AddDbContext<IdentityDataContext>(options =>
                    options.UseNpgsql(connectionString, o => 
                    {
                        o.SetPostgresVersion(9, 6);
                        o.MigrationsHistoryTable("__MCSPMigrationsHistory", "mcsp");
                    })
                );
            });
        }
    }
}