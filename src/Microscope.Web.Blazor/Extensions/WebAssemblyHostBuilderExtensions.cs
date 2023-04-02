using Blazored.LocalStorage;
using Microscope.Admin.Core.Handlers;
using Microscope.Admin.Managers;
using Microscope.Admin.Managers.Preferences;
using Microscope.SDK.Dotnet;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Microscope.Web.Blazor.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        private const string ClientName = "Microscope.API";

        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");

            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            var baseAddress = builder.Configuration.GetValue<string>("APIBaseAddress");
            Console.WriteLine(baseAddress);

            builder
                .Services
                .AddAuthentication(builder)
                .AddBlazoredLocalStorage()
                .AddLocalization(options =>
                {
                    options.ResourcesPath = "Resources";
                })
                .AddMudServices(config =>
                {
                    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                    config.SnackbarConfiguration.PreventDuplicates = false;
                    config.SnackbarConfiguration.NewestOnTop = false;
                    config.SnackbarConfiguration.ShowCloseIcon = true;
                    config.SnackbarConfiguration.VisibleStateDuration = 3000;
                    config.SnackbarConfiguration.HideTransitionDuration = 100;
                    config.SnackbarConfiguration.ShowTransitionDuration = 100;
                    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;

                })
                .AddScoped<PreferenceManager>() // CHECK BLAZORHERO
                .AddManagers()
                // .AddScoped(sp => sp
                //     .GetRequiredService<IHttpClientFactory>()
                //     .CreateClient(ClientName).EnableIntercept(sp))
                // .AddHttpClient(ClientName, client => client.BaseAddress = new Uri(baseAddress))
                .AddTransient<AuthenticationHeaderHandler>()
                .AddHttpClient<MicroscopeClient>(client => client.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();

            builder.Services.AddHttpClientInterceptor();
            
            return builder;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services,WebAssemblyHostBuilder builder)
        {
            services.AddOidcAuthentication(options =>
                {
                    builder.Configuration.Bind("Auth", options.ProviderOptions);
                    options.ProviderOptions.ResponseType = "code";
                    options.ProviderOptions.DefaultScopes.Add("roles");
                });
            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }
    }
}