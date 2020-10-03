using GraphQL.Types;
using GraphQL;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

public class IdentityMutation : ObjectGraphType<object>, IGraphMutationMarker
{
    public IdentityMutation()
    {
        FieldAsync<RoleType>(
            "UpdateRole",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var roleManager =  scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var role = await roleManager.FindByIdAsync(id.ToString());
                    role.Name = context.GetArgument<string>("name").ToString();

                    await roleManager.UpdateAsync(role);
                    return role;

                }

            });

        FieldAsync<UserType>(
            "DeleteUser",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var user = await userManager.FindByIdAsync(id.ToString());

                    if (user != null)
                    {
                        await userManager.DeleteAsync(user);
                    }

                    return user;

                }

            });

        FieldAsync<RoleType>(
            "DeleteRole",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var role = await roleManager.FindByIdAsync(id.ToString());

                    if (role != null)
                    {
                        await roleManager.DeleteAsync(role);
                    }

                    return role;

                }

            });

        FieldAsync<RoleType>(
            "CreateRole",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
            ),
            resolve: async context =>
            {
                var name = context.GetArgument<string>("name");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var role = new IdentityRole();
                    role.Name = name;

                    var dto = await roleManager.CreateAsync(role);
                    return role;

                }

            });
    }
}