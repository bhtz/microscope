using Microsoft.Extensions.DependencyInjection;
using GraphQL.Authorization;
using GraphQL.Validation;
using System;
using Microscope.Authorization.Graphql;

public static class GraphQLAuthConfiguration
    {   
        public static void AddGraphQLAuth(this IServiceCollection services, Action<AuthorizationSettings> configure)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();
            services.AddTransient<IValidationRule, PermissionValidationRule>();

            services.AddTransient(s =>
            {
                var authSettings = new AuthorizationSettings();
                configure(authSettings);
                return authSettings;
            });
 

        }

    }