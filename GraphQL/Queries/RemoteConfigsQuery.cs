using GraphQL.Authorization;
using GraphQL.Types;
using Microscope.Data;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

public class RemoteConfigsQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public RemoteConfigsQuery(IronHasuraDbContext dbContext)
    {    
        this.AuthorizeWith("AdminPolicy");

        FieldAsync<ListGraphType<RemoteConfigType>>("RemoteConfigs", resolve: async context => 
        {
             var userContext = context.UserContext as GraphQLUserContext;
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