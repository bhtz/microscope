using GraphQL.Types;
using GraphQL;
using Microscope.Data;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;


public class AnalyticMutation : ObjectGraphType<object>, IGraphMutationMarker
{
    public AnalyticMutation()
    {
        FieldAsync<AnalyticType>(
            "UpdateAnalytic",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<AnalyticsInputType>> { Name = "analytic" }
            ),
            resolve: async context =>
            {
                var analyticInput = context.GetArgument<Analytic>("analytic");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>();
                    var analytic = await dbContext.Analytic.FindAsync(analyticInput.Id);

                    analytic.Key = analyticInput.Key;
                    analytic.Dimension = analyticInput.Dimension;

                    dbContext.Analytic.Update(analytic);
                    dbContext.SaveChanges();

                    return analytic;
                }

            });


        FieldAsync<AnalyticType>(
            "DeleteAnalytic",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>();
                    var analytic = await dbContext.Analytic.FindAsync(id);

                    if (analytic != null)
                    {
                        dbContext.Remove(analytic);
                        dbContext.SaveChanges();
                    }

                    return analytic;

                }

            });

        Field<AnalyticType>(
            "CreateAnalytic",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<AnalyticsInputType>> { Name = "analytic" }
            ),
            resolve: context =>
            {
                var analytic = context.GetArgument<Analytic>("analytic");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>();

                    dbContext.Add(analytic);
                    dbContext.SaveChanges();

                    return analytic;

                }

            });
    }
}