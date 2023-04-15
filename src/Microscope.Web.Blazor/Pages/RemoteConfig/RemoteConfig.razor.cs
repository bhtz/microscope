using Microscope.Core.Queries.RemoteConfig;
using Microscope.Web.Blazor.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using static Microscope.Web.Blazor.Pages.RemoteConfig.RemoteConfigFormDialog;

namespace Microscope.Web.Blazor.Pages.RemoteConfig
{
    public partial class RemoteConfig : ComponentBase
    {
        #region injected properties

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        #endregion

        #region properties
        public IList<FilteredRemoteConfigQueryResult> RemoteConfigs { get; set; } = new List<FilteredRemoteConfigQueryResult>();
        public string SearchTerm { get; set; } = String.Empty;
        private bool IsLoading { get; set; } = false;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            await this.GetRemoteConfigs();
        }

        private async Task GetRemoteConfigs()
        {
            IsLoading = true;
            IEnumerable<FilteredRemoteConfigQueryResult> remotes = await _microscopeClient.GetRemoteConfigsAsync();
            this.RemoteConfigs = remotes.ToList();
            IsLoading = false;
        }
        
        private Func<FilteredRemoteConfigQueryResult, bool> FilterFunc => x =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
        
            if (x.Key.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        private async Task OpenCreateDialog()
        {

            var dialog = _dialogService.Show<RemoteConfigFormDialog>("Modal", new DialogOptions
            {
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseButton = true,
                DisableBackdropClick = true
            });

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var newItem = (RemoteConfigFormViewModel)result.Data;
                //In a real world scenario we would reload the data from the source
                FilteredRemoteConfigQueryResult newRemoteConfig = new FilteredRemoteConfigQueryResult
                {
                    Id = newItem.Id,
                    Key = newItem.Key,
                    Dimension = newItem.Dimension
                };

                this.RemoteConfigs.Add(newRemoteConfig);

            }
        }

        private async Task OnSelectItem(FilteredRemoteConfigQueryResult item)
        {

            RemoteConfigFormViewModel dto = new RemoteConfigFormViewModel
            {
                Id = item.Id,
                Key = item.Key,
                Dimension = item.Dimension
            };

            var parameters = new DialogParameters { ["RemoteConfig"] = dto };

            //await this.JSONEditor();

            var dialog = _dialogService.Show<RemoteConfigFormDialog>("Modal", parameters, new DialogOptions
            {
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseButton = true,
                DisableBackdropClick = true
            });
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                //In a real world scenario we would reload the data 

                var editedItem = (RemoteConfigFormViewModel)result.Data;

                var remoteConfigToUpdate = this.RemoteConfigs.FirstOrDefault(a => a.Id == editedItem.Id);
                if (remoteConfigToUpdate != null)
                {
                    remoteConfigToUpdate.Key = editedItem.Key;
                    remoteConfigToUpdate.Dimension = editedItem.Dimension;
                }

            }
        }
        private async Task Delete(FilteredRemoteConfigQueryResult item)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Are you sure ?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = _dialogService.Show<ConfirmDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var res = await this._httpClient.DeleteAsync("api/remoteconfig/" + item.Id);
                if (res.IsSuccessStatusCode)
                {
                    this.RemoteConfigs.Remove(item);
                    _snackBar.Add("Remote Config deleted", Severity.Success);
                }
                else
                {
                     _snackBar.Add("Error", Severity.Error);
                }
            }

        }

    }
}