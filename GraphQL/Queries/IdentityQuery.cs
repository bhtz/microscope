using GraphQL.Types;
using GraphQL;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;


public class IdentityQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public IdentityQuery()
    {

        FieldAsync<ListGraphType<UserType>>("Users", resolve: async context =>
        {
            using (var scope = context.RequestServices.CreateScope())
            {
                var  userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                return await userManager.Users.ToListAsync();
            }

        });

        FieldAsync<UserType>(
            "UserById",
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    return await userManager.FindByIdAsync(id);
                }

            });

        FieldAsync<ListGraphType<RoleType>>("Roles", resolve: async context =>
        {
            using (var scope = context.RequestServices.CreateScope())
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                return await roleManager.Roles.ToListAsync();
            }

        });

        FieldAsync<RoleType>(
            "RoleById",
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    return await roleManager.FindByIdAsync(id);
                }

            });
    }
}