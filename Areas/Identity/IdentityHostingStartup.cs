using System;
using com.ironhasura.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(com.ironhasura.Areas.Identity.IdentityHostingStartup))]
namespace com.ironhasura.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityDataContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("MCSP_IDENTITY_CS"), o => 
                        {
                            o.SetPostgresVersion(9, 6);
                            o.MigrationsHistoryTable("__MCSPMigrationsHistory", "mcsp");
                        }
                    )
                );
            });
        }
    }
}