using System.Threading.Tasks;
using Microscope.Admin.Settings;
using MudBlazor;

namespace Microscope.Admin.Managers.Preferences
{
   public interface IPreferenceManager
    {
        Task SetPreference(Preference preference);

        Task<Preference> GetPreference();

        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();

        Task ChangeLanguageAsync(string languageCode);
    }
}