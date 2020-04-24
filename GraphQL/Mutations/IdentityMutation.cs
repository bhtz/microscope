using GraphQL.Types;
using IronHasura.Data;
using IronHasura.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class IdentityMutation : ObjectGraphType<object>
{
    public IdentityMutation(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {   
        FieldAsync<RoleType>(
            "UpdateRole",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<IdGraphType>("id");
                var role = await roleManager.FindByIdAsync(id.ToString());

                role.Name = context.GetArgument<IdGraphType>("name").ToString();

                return roleManager.UpdateAsync(role);
            });
    }
}