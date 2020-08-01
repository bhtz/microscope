using GraphQL;
using GraphQL.Server;
using Microscope.GraphQL;
using Microscope.Authorization.Graphql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Microscope.Configurations
{
    public static class GraphQLConfiguration
    {
        public static IServiceCollection AddGraphQLConfiguration(this IServiceCollection services, IWebHostEnvironment hostingEnvironment)
        {
            services.AddTransient<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<MicroscopeSchema>();
            services.AddSingleton<RootQuery>();
            services.AddSingleton<RootMutation>();


            services.AddSingleton<IGraphQueryMarker, IdentityQuery>();
            services.AddSingleton<IGraphQueryMarker, RemoteConfigsQuery>();
            services.AddSingleton<IGraphQueryMarker, AnalyticsQuery>();

            services.AddSingleton<IGraphMutationMarker, IdentityMutation>();
            services.AddSingleton<IGraphMutationMarker, RemoteConfigsMutation>();
            services.AddSingleton<IGraphMutationMarker, AnalyticMutation>();


            services.AddGraphQLAuth(options =>
            {
                options.AddPolicy("AuthenticatedPolicy", p => p.AddRequirement(new AuthenticatedAuthorizationRequirement()));
                options.AddPolicy("AdminPolicy", p => p.RequireClaim("role", "Admin"));

            });

            services
                .AddGraphQL(o =>
                 {
                     o.EnableMetrics = true;
                     o.ExposeExceptions = hostingEnvironment.IsDevelopment();

                 })
                .AddGraphTypes()
                .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User });

            return services;
        }
    }
}