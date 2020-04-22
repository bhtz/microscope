using System.Collections.Generic;
using GraphQL.Types;
using IronHasura.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class RootQuery : ObjectGraphType<object>
{
    public RootQuery(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        Name = "Query";
        
        Field<StringGraphType>("hello", resolve: (context) => "hello world !");
        
        FieldAsync<ListGraphType<UserType>>("IronHasuraUsers", resolve: async context => 
        {
            return await userManager.Users.ToListAsync();
        });

        FieldAsync<ListGraphType<RoleType>>("IronHasuraRoles", resolve: async context => 
        {
            return await roleManager.Roles.ToListAsync();
        });
    }
}