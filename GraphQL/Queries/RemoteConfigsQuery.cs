using GraphQL.Types;
using IronHasura.Data;
using IronHasura.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class RemoteConfigsQuery : ObjectGraphType<object>
{
    public RemoteConfigsQuery(IronHasuraDbContext dbContext)
    {
        Name = "RemoteConfigs";
        
        FieldAsync<ListGraphType<RemoteConfigType>>("GetAll", resolve: async context => 
        {
            return await dbContext.RemoteConfig.ToListAsync();
        });
    }
}