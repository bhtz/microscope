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
            services.AddScoped<IDependencyResolver>(x => new FuncDependencyResolver(x.GetRequiredService));
            services.AddScoped<IronHasuraSchema>();
            services.AddScoped<RootQuery>();
            services
                .AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);

            return services;
        }
    }
}