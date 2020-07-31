using GraphQL;
using GraphQL.Server;
using Microscope.GraphQL;
using Microscope.Authorization.Graphql;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Configurations
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

            services.AddGraphQLAuth(options =>
            {
                options.AddPolicy("AuthenticatedPolicy", p => p.AddRequirement(new AuthenticatedAuthorizationRequirement()));
                options.AddPolicy("AdminPolicy", p => p.RequireClaim("role", "Admin"));

            });

            services
                .AddGraphQL(o => { o.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped);

            return services;
        }
    }
}