using Microscope.Admin.Settings;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Microscope.Web.Blazor.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        MudTheme currentTheme;

        public bool _drawerOpen = true;

        protected override async Task OnInitializedAsync()
        {
            currentTheme = await _preferenceManager.GetCurrentThemeAsync();
        }


        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        async Task DarkMode()
        {
            bool isDarkMode = await _preferenceManager.ToggleDarkModeAsync();
            if (isDarkMode)
            {
                currentTheme = Theme.DefaultTheme;
            }
            else
            {
                currentTheme = Theme.DarkTheme;
            }
        }
    }
}
