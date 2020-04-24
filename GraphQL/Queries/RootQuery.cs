using GraphQL.Types;
using IronHasura.Data;
using IronHasura.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class RootQuery : ObjectGraphType<object>
{
    public RootQuery(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IronHasuraDbContext dbContext)
    {
        Name = "Query";
        
        Field<IdentityQuery>("Identity", resolve: context => new {});
        
        Field<RemoteConfigsQuery>("RemoteConfigs", resolve: context => new {});

        FieldAsync<ListGraphType<AnalyticType>>("Analytics", resolve: async context => 
        {
            return await dbContext.Analytic.ToListAsync();
        });
    }
}