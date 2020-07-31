using GraphQL.Types;
using Microscope.Data;
using Microscope.GraphQL;
using Microscope.GraphQL.Types;

public class RemoteConfigsMutation : ObjectGraphType<object>, IGraphMutationMarker
{
    public RemoteConfigsMutation(IronHasuraDbContext dbContext)
    {
        FieldAsync<RemoteConfigType>(
            "UpdateRemoteConfig",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<RemoteConfigType>> { Name = "config" }
            ),
            resolve: async context =>
            {
                var configInput = context.GetArgument<RemoteConfig>("config");
                var remoteConfig = await dbContext.RemoteConfig.FindAsync(configInput.Id);
                
                remoteConfig.Key = configInput.Key;
                remoteConfig.Value = configInput.Value;

                dbContext.RemoteConfig.Update(remoteConfig);
                dbContext.SaveChanges();

                return remoteConfig;
            });


        FieldAsync<RoleType>(
            "DeleteRemoteConfig",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            ),
            resolve: async context =>
            {
                var id = context.GetArgument<string>("id");
                var config = await dbContext.RemoteConfig.FindAsync(id);

                if(config != null) 
                {
                    dbContext.Remove(config);
                    dbContext.SaveChanges();
                }

                return config;
            });

        Field<RemoteConfigType>(
            "CreateRemoteConfig",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<RemoteConfigType>> { Name = "config" }
            ),
            resolve: context =>
            {
                var remoteConfig = context.GetArgument<RemoteConfig>("config");
            
                dbContext.Add(remoteConfig);
                dbContext.SaveChanges();

                return remoteConfig;
            });
    }
}