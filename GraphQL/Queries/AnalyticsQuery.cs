using GraphQL.Types;
using IronHasura.Data;
using IronHasura.GraphQL;
using IronHasura.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

public class AnalyticsQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public AnalyticsQuery(IronHasuraDbContext dbContext)
    {        
        FieldAsync<ListGraphType<AnalyticType>>("Analytics", resolve: async context => 
        {
            return await dbContext.Analytic.ToListAsync();
        });

        FieldAsync<AnalyticType>("AnalyticsById", 
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), 
            resolve: async context => 
            {
                var id = context.GetArgument<string>("id");
                return await dbContext.Analytic.FindAsync(id);
            });
    }
}