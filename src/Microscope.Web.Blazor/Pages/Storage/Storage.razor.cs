using Microscope.Admin.Shared.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using static Microscope.Web.Blazor.Pages.Storage.ContainerFormDialog;

namespace Microscope.Web.Blazor.Pages.Storage
{
    public partial class Storage : ComponentBase
    {
        #region injected properties

        [Inject]
        private IJSRuntime JsRuntime { get; set; }


        #endregion

        #region private properties

        public string SearchTerm { get; set; } = String.Empty;
        private string _selectedContainer;
        #endregion

        #region public properties

        public List<string> Blobs { get; set; } = new List<string> { };
        public List<string> Containers { get; set; } = new List<string> { };
        public bool IsLoading { get; set; } = false;
        public string SelectedContainer
        {
            get => _selectedContainer;
            set
            {
                _selectedContainer = value;
                this.GetBlobsFromSelectedContainer();
            }
        }

        #endregion

        #region methods

        protected override async Task OnInitializedAsync()
        {
            await this.GetContainers();
        }

        private async Task GetContainers()
        {
            var containerResults = await _microscopeClient.GetContainersAsync();
            this.Containers = containerResults.ToList();
            this.SelectedContainer = this.Containers.FirstOrDefault();
        }

        private bool FilterFunc(string element)
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            if (element.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private async Task OpenCreateContainerDialog()
        {

            var dialog = _dialogService.Show<ContainerFormDialog>("Modal", new DialogOptions
            {
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseButton = true,
                DisableBackdropClick = true
            });

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var newItem = (StorageContainerDTO)result.Data;
                this.Containers.Add(newItem.Name);
                this.SelectedContainer = newItem.Name;
                this.StateHasChanged();
            }
        }

        private async void GetBlobsFromSelectedContainer()
        {
            if (!string.IsNullOrEmpty(this.SelectedContainer))
            {
                this.SearchTerm = string.Empty;
                var blobResults = await _microscopeClient.GetBlobsAsync(this.SelectedContainer);
                this.Blobs = blobResults.ToList();
                this.StateHasChanged();
            }
        }

        private async void Download(string blobName)
        {
            var blobByteArray = await _microscopeClient.GetBlobAsync(this.SelectedContainer, blobName);

            await this.JsRuntime.InvokeVoidAsync("interop.downloadFromByteArray",
                new
                {
                    ByteArray = blobByteArray,
                    FileName = blobName,
                    ContentType = "application/octet-stream"
                });
        }

        private async Task DeleteBlob(string blobName)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Are you sure ?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = _dialogService.Show<ConfirmDialog>("Delete Blob", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var success = await _microscopeClient.DeleteBlobAsync(this.SelectedContainer, blobName);

                if (success)
                {
                    this.Blobs.Remove(blobName);
                    _snackBar.Add("Blob deleted", Severity.Success);
                }
                else
                {
                    _snackBar.Add("Error deleted blob", Severity.Error);
                }
            }
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            IBrowserFile file = e.File;

            this.IsLoading = true;

            string imageType = file.ContentType;
            var fileContent = new StreamContent(file.OpenReadStream(50 * 1000 * 1024)); // LIMIT 50 MO
            var content = new MultipartFormDataContent();

            content.Add(content: fileContent, name: "file", fileName: file.Name);

            try
            {
                bool success = await _microscopeClient.PostBlobsAsync(this.SelectedContainer,content);
                if (success)
                {
                    _snackBar.Add("File uploaded", Severity.Success);
                    this.GetBlobsFromSelectedContainer();
                }
                else
                {
                    _snackBar.Add("res.ReasonPhrase", Severity.Error);
                }
            }
            catch (System.Exception ex)
            {
                _snackBar.Add(ex.Message, Severity.Error);

            }

            this.IsLoading = false;
        }

        #endregion
    }
}