using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.PostgreSql;
using Elsa.Persistence.EntityFramework.SqlServer;
using Microscope.Workflow.Workflows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Workflow;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkflow(this IServiceCollection services, IConfiguration configuration)
    {
        var brandName = "Microscope";
        var provider = configuration.GetValue<string>("DatabaseProvider");
        var connectionString = configuration.GetConnectionString(brandName);
        
        services
            .AddElsa(elsa => elsa
                .UseEntityFrameworkPersistence(options =>
                {
                    switch (provider)
                    {
                        case "postgres":
                            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                            options.UsePostgreSql(connectionString);
                            break;

                        case "mssql":
                            options.UseSqlServer(connectionString);
                            break;
                        
                        // elsa configured by default in-memory
                    }
                }, true)
                .AddConsoleActivities()
                .AddHttpActivities(configuration.GetSection("Workflow:Http").Bind)
                .AddQuartzTemporalActivities()
                .AddJavaScriptActivities()
                .AddWorkflow<HeartbeatWorkflow>()
            );
        
        services.AddElsaApiEndpoints();

        return services;
    }
}