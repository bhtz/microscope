using Microscope.Admin.Shared.Dialogs;
using Microscope.Application.Features.Analytic.Queries;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static Microscope.Web.Blazor.Pages.Analytic.AnalyticFormDialog;

namespace Microscope.Web.Blazor.Pages.Analytic
{
    public partial class Analytic : ComponentBase
    {
        #region properties
        public IList<AnalyticQueryResult> Analytics { get; set; } = new List<AnalyticQueryResult>();

        public string SearchTerm { get; set; } = String.Empty;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            await this.GetAnalytic();
        }

        private async Task GetAnalytic()
        {
            IEnumerable<AnalyticQueryResult> analytics = await _microscopeClient.GetAnalyticsAsync();
            this.Analytics = analytics.ToList();
        }

        private bool FilterFunc(AnalyticQueryResult element)
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            if (element.Key.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private async Task OpenCreateDialog()
        {

            var dialog = _dialogService.Show<AnalyticFormDialog>("Modal", new DialogOptions
            {
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseButton = true,
                DisableBackdropClick = true
            });

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var newItem = (AnalyticFormViewModel)result.Data;
                //In a real world scenario we would reload the data from the source
                AnalyticQueryResult newAnalytic = new AnalyticQueryResult
                {
                    Id = newItem.Id,
                    Key = newItem.Key,
                    Dimension = newItem.Dimension
                };

                this.Analytics.Add(newAnalytic);
            }
        }

        private async Task OnSelectItem(AnalyticQueryResult item)
        {

            AnalyticFormViewModel dto = new AnalyticFormViewModel
            {
                Id = item.Id,
                Key = item.Key,
                Dimension = item.Dimension
            };

            var parameters = new DialogParameters { ["Analytic"] = dto };

            var dialog = _dialogService.Show<AnalyticFormDialog>("Modal", parameters, new DialogOptions
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

                var editedItem = (AnalyticFormViewModel)result.Data;

                var analyticToUpdate = this.Analytics.FirstOrDefault(a => a.Id == editedItem.Id);
                if (analyticToUpdate != null)
                {
                    analyticToUpdate.Key = editedItem.Key;
                    analyticToUpdate.Dimension = editedItem.Dimension;
                }

            }
        }

        private async Task Delete(AnalyticQueryResult item)
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
                var res = await this._httpClient.DeleteAsync("api/analytic/" + item.Id);

                if (res.IsSuccessStatusCode)
                {
                    this.Analytics.Remove(item);
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