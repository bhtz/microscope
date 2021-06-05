using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microscope.Admin.Extensions;
using Microscope.Admin.Managers.Preferences;
using System.Globalization;

namespace Microscope.Admin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder
                 .CreateDefault(args)
                 .AddRootComponents()
                 .AddClientServices();

            var host = builder.Build();
            var storageService = host.Services.GetRequiredService<PreferenceManager>();
            if (storageService != null)
            {
                CultureInfo culture;
                var preference = await storageService.GetPreference();
                if (preference != null)
                    culture = new CultureInfo(preference.LanguageCode);
                else
                    culture = new CultureInfo("fr-FR");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            await builder.Build().RunAsync();
        }
    }
}
