using Blazored.LocalStorage;
using Microscope.Admin.Managers;
using Microscope.SDK.Dotnet;
using Microscope.Web.Blazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

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
            Uri apiAddress;
            var baseAddressConfiguration = builder.Configuration.GetValue<string>("APIBaseAddress");

            if (String.IsNullOrEmpty(baseAddressConfiguration))
                apiAddress = new Uri(builder.HostEnvironment.BaseAddress);
            else
                apiAddress = new Uri(baseAddressConfiguration);

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
                .AddScoped<PreferenceService>()
                .AddManagers()
                .AddTransient<AuthenticationHeaderHandler>()
                .AddHttpClient<MicroscopeClient>(client => client.BaseAddress = apiAddress)
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();
            
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient());

            return builder;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services,WebAssemblyHostBuilder builder)
        {
            services.AddOidcAuthentication(options =>
                {
                    string tenant = "master";
                    var subDomain = GetSubDomain(new Uri(builder.HostEnvironment.BaseAddress));

                    // if (!string.IsNullOrEmpty(subDomain))
                    //     tenant = subDomain.Split('.')[0];
                    //     //tenant = subDomain;
            
                    var configKey = $"OIDC:{tenant}";
                    builder.Configuration.Bind(configKey, options.ProviderOptions);

                    options.ProviderOptions.ResponseType = "code";
                    options.ProviderOptions.DefaultScopes.Add("roles");
                    options.UserOptions.RoleClaim = "roles";
                })
                .AddAccountClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();
            
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
        
        private static string GetSubDomain(Uri url)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                var host = url.Host;
                if (host.Split('.').Length > 2)
                {
                    var lastIndex = host.LastIndexOf(".");
                    var index = host.LastIndexOf(".", lastIndex - 1);
                    return host.Substring(0, index);
                }
            }

            return null;
        }
    }
}