using Microscope.Admin.Settings;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Microscope.Web.Blazor.Shared;

public partial class MainLayout : LayoutComponentBase
{
    MudTheme currentTheme;
    private Guid _subscriptionId;
    public bool _drawerOpen = false;
    public DrawerVariant DrawerConfig = DrawerVariant.Mini;

    protected override async Task OnInitializedAsync()
    {
        currentTheme = await _preferenceService.GetCurrentThemeAsync();
        _drawerOpen = await _preferenceService.GetDrawerPreferenceAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var breakpoint = await BreakpointListener.GetBreakpoint();
            DrawerConfig = breakpoint == Breakpoint.Xs ? DrawerVariant.Responsive : DrawerVariant.Mini;
            
            var subscriptionResult = await BreakpointListener.Subscribe(breakpoint =>
            {
                DrawerConfig = breakpoint == Breakpoint.Xs ? DrawerVariant.Responsive : DrawerVariant.Mini;
                StateHasChanged();
            });

            _subscriptionId = subscriptionResult.SubscriptionId;
        }

        await base.OnAfterRenderAsync(firstRender);
    }
    
    public async ValueTask DisposeAsync() => await BreakpointListener.Unsubscribe(_subscriptionId);

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
        _preferenceService.ToggleDrawerAsync();
    }

    async Task DarkMode()
    {
        bool isDarkMode = await _preferenceService.ToggleDarkModeAsync();
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