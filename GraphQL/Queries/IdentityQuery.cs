using GraphQL.Types;
using IronHasura.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class IdentityQuery : ObjectGraphType<object>
{
    public IdentityQuery(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {        
        FieldAsync<ListGraphType<UserType>>("Users", resolve: async context => 
        {
            return await userManager.Users.ToListAsync();
        });

        FieldAsync<UserType>(
            "UserById", 
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), 
            resolve: async context => 
            {
                var id = context.GetArgument<string>("id");
                return await userManager.FindByIdAsync(id);
            });

        FieldAsync<ListGraphType<RoleType>>("Roles", resolve: async context => 
        {
            return await roleManager.Roles.ToListAsync();
        });

        FieldAsync<RoleType>(
            "RoleById", 
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), 
            resolve: async context => 
            {
                var id = context.GetArgument<string>("id");
                return await roleManager.FindByIdAsync(id);
            });
    }
}