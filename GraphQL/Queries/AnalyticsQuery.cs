using GraphQL.Types;
using Microscope.Data;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using GraphQL;
using Microsoft.Extensions.DependencyInjection;
using System;


public class AnalyticsQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public AnalyticsQuery()
    {
        FieldAsync<ListGraphType<AnalyticType>>("Analytics", resolve: async context =>
        {
            using (var scope = context.RequestServices.CreateScope())
            {
                return await scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>().Analytic.ToListAsync();

            }

        });

        FieldAsync<AnalyticType>("AnalyticsById",
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    return await scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>().Analytic.FindAsync(id);
                }
            });
    }
}