using System.ComponentModel.DataAnnotations;
using Microscope.Application.Features.Storage.Commands;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Microscope.Web.Blazor.Pages.Storage
{
    public partial class ContainerFormDialog
    {

        #region injected properties

        #endregion

        #region properties
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public StorageContainerDTO StorageContainer { get; set; } = new StorageContainerDTO();

        public bool Success { get; set; }

        #endregion

        public async Task OnValidSubmit()
        {
            Success = true;
            StateHasChanged();

            var command = new AddContainerCommand { Name = this.StorageContainer.Name };
            bool success = await _microscopeClient.PostContainerAsync(command);

            if (success)
            {
                _snackBar.Add(localizer["Container Saved"], Severity.Success);
                MudDialog.Close(DialogResult.Ok(this.StorageContainer));
            }
            else
            {
                _snackBar.Add("Error add cotainer", Severity.Error);
                MudDialog.Close(DialogResult.Cancel());
            }
        }

        void Cancel() => MudDialog.Cancel();

        public class StorageContainerDTO
        {
            private string _name;

            [Required]
            [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
            public string Name
            {
                get { return _name; }
                set { _name = value.ToLower(); }
            }

        }
    }
}