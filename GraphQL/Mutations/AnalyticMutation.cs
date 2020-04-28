using GraphQL.Types;
using IronHasura.Data;
using IronHasura.GraphQL;
using IronHasura.GraphQL.Types;

public class AnalyticMutation : ObjectGraphType<object>, IGraphMutationMarker
{
    public AnalyticMutation(IronHasuraDbContext dbContext)
    {
        FieldAsync<AnalyticType>(
            "UpdateAnalytic",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<AnalyticType>> { Name = "analytic" }
            ),
            resolve: async context =>
            {
                var analyticInput = context.GetArgument<Analytic>("analytic");
                var analytic = await dbContext.Analytic.FindAsync(analyticInput.Id);
                
                analytic.Key = analyticInput.Key;
                analytic.Dimension = analyticInput.Dimension;

                dbContext.Analytic.Update(analytic);
                dbContext.SaveChanges();

                return analytic;
            });


        FieldAsync<AnalyticType>(
            "DeleteAnalytic",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");
                var analytic = await dbContext.Analytic.FindAsync(id);

                if(analytic != null) 
                {
                    dbContext.Remove(analytic);
                    dbContext.SaveChanges();
                }

                return analytic;
            });

        Field<AnalyticType>(
            "CreateAnalytic",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<AnalyticType>> { Name = "analytic" }
            ),
            resolve: context =>
            {
                var analytic = context.GetArgument<Analytic>("analytic");
            
                dbContext.Add(analytic);
                dbContext.SaveChanges();

                return analytic;
            });
    }
}