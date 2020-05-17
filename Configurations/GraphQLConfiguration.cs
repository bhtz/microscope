using GraphQL;
using GraphQL.Server;
using IronHasura.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace IronHasura.Configurations
{
    public static class GraphQLConfiguration
    {
        public static IServiceCollection AddGraphQLConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IGraphQueryMarker, IdentityQuery>();
            services.AddScoped<IGraphQueryMarker, RemoteConfigsQuery>();
            services.AddScoped<IGraphQueryMarker, AnalyticsQuery>();
            
            services.AddScoped<IGraphMutationMarker, IdentityMutation>();
            services.AddScoped<IGraphMutationMarker, RemoteConfigsMutation>();
            services.AddScoped<IGraphMutationMarker, AnalyticMutation>();

            services.AddScoped<RootQuery>();
            services.AddScoped<RootMutation>();

            services.AddScoped<IronHasuraSchema>();
            services.AddScoped<IDependencyResolver>(x => new FuncDependencyResolver(x.GetRequiredService));
            
            services
                .AddGraphQL(o => { o.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped);

            return services;
        }
    }
}