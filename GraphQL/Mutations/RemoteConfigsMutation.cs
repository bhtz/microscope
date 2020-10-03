using GraphQL.Types;
using GraphQL;
using Microscope.Data;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

public class RemoteConfigsMutation : ObjectGraphType<object>, IGraphMutationMarker
{
    public RemoteConfigsMutation(IServiceProvider serviceProvider)
    {
        FieldAsync<RemoteConfigType>(
            "UpdateRemoteConfig",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<RemoteConfigInputType>> { Name = "config" }
            ),
            resolve: async context =>
            {
                var configInput = context.GetArgument<RemoteConfig>("config");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>();
                    var remoteConfig = await dbContext.RemoteConfig.FindAsync(configInput.Id);

                    remoteConfig.Key = configInput.Key;
                    remoteConfig.Value = configInput.Value;

                    dbContext.RemoteConfig.Update(remoteConfig);
                    dbContext.SaveChanges();

                    return remoteConfig;

                }

            });


        FieldAsync<RoleType>(
            "DeleteRemoteConfig",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");


                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>();
                    var config = await dbContext.RemoteConfig.FindAsync(id);

                    if (config != null)
                    {
                        dbContext.Remove(config);
                        dbContext.SaveChanges();
                    }

                    return config;

                }

            });

        Field<RemoteConfigType>(
            "CreateRemoteConfig",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<RemoteConfigInputType>> { Name = "config" }
            ),
            resolve: context =>
            {
                var remoteConfig = context.GetArgument<RemoteConfig>("config");

                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IronHasuraDbContext>();

                    dbContext.RemoteConfig.Add(remoteConfig);
                    dbContext.SaveChanges();

                    return remoteConfig;

                }

            });
    }
}