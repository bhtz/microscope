using GraphQL.Types;
using IronHasura.Data;
using IronHasura.GraphQL;
using IronHasura.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

public class RemoteConfigsQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public RemoteConfigsQuery(IronHasuraDbContext dbContext)
    {        
        FieldAsync<ListGraphType<RemoteConfigType>>("RemoteConfigs", resolve: async context => 
        {
            return await dbContext.RemoteConfig.ToListAsync();
        });

        FieldAsync<UserType>("RemoteConfigById", 
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), 
            resolve: async context => 
            {
                var id = context.GetArgument<string>("id");
                return await dbContext.RemoteConfig.FindAsync(id);
            });
    }
}