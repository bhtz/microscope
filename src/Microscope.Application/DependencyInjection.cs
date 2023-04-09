using System.Reflection;
using FluentValidation;
using MediatR;
using Microscope.Application.Common.Behaviors;
using Microscope.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMicroscopeApplication(this IServiceCollection services)
    {
        var execAssembly = Assembly.GetExecutingAssembly();
        var featuresAssembly = typeof(IMicroscopeModule).GetTypeInfo().Assembly;

        services.AddAutoMapper(execAssembly);
        services.AddValidatorsFromAssembly(featuresAssembly);
        services.AddMediatR(x => x.RegisterServicesFromAssembly(featuresAssembly));
        services.AddMediatR(x => x.RegisterServicesFromAssembly(execAssembly));

        // services.AddSingleton<IAuthorizationHandler, GridOwnedByRequirementHandler>();
        // services.AddSingleton<IAuthorizationHandler, GridCreatedByRequirementHandler>();
        // services.AddSingleton<IAuthorizationHandler, TemplateOwnedByRequirementHandler>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}