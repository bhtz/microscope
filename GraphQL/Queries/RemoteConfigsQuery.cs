using GraphQL.Authorization;
using GraphQL;
using GraphQL.Types;
using Microscope.Data;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public class RemoteConfigsQuery : ObjectGraphType<object>, IGraphQueryMarker
{
    public RemoteConfigsQuery()
    {
        //this.AuthorizeWith("AdminPolicy");

        FieldAsync<ListGraphType<RemoteConfigType>>("RemoteConfigs", resolve: async context =>
        {
            //var userContext = context.UserContext as GraphQLUserContext;

            using (var scope = context.RequestServices.CreateScope())
            {
                return await scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>().RemoteConfig.ToListAsync();
            }

        }).AuthorizeWith("AdminPolicy");

        FieldAsync<UserType>("RemoteConfigById",
            arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");

                using (var scope = context.RequestServices.CreateScope())
                {
                    return await scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>().RemoteConfig.FindAsync(id);

                }
            });
    }
}