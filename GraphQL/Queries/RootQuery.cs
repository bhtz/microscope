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
        
        FieldAsync<ListGraphType<UserType>>("IronHasuraUsers", resolve: async context => 
        {
            return await userManager.Users.ToListAsync();
        });

        FieldAsync<ListGraphType<RoleType>>("IronHasuraRoles", resolve: async context => 
        {
            return await roleManager.Roles.ToListAsync();
        });

        FieldAsync<ListGraphType<RemoteConfigType>>("RemoteConfigs", resolve: async context => 
        {
            return await dbContext.RemoteConfig.ToListAsync();
        });

        FieldAsync<ListGraphType<AnalyticType>>("Analytics", resolve: async context => 
        {
            return await dbContext.Analytic.ToListAsync();
        });
    }
}