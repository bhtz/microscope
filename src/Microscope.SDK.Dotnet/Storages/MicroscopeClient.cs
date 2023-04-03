using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microscope.SDK.Dotnet.Routes;
using Microscope.Features.Storage.Commands;
using System.Net.Http;

namespace Microscope.SDK.Dotnet
{
    public partial class MicroscopeClient
    {
        public async Task<bool> PostContainerAsync(AddContainerCommand command)
        {
            var response = await this._httpClient.PostAsJsonAsync(StoragesEndpoint.CreateContainer, command);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<string>> GetContainersAsync()
        {
            return await this._httpClient.GetFromJsonAsync<IEnumerable<string>>(StoragesEndpoint.GetAllContainer);
        }

        public async Task<bool> PostBlobsAsync(string container, MultipartFormDataContent blob)
        {
            var response = await this._httpClient.PostAsync(StoragesEndpoint.PostBlobInContainer(container), blob);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<string>> GetBlobsAsync(string container)
        {
            return await this._httpClient.GetFromJsonAsync<IEnumerable<string>>(StoragesEndpoint.GetAllBlobsInContainer(container));
        }

        public async Task<byte[]> GetBlobAsync(string container, string blob)
        {
            var response = await this._httpClient.GetAsync(StoragesEndpoint.GetBlobInContainer(container, blob));
            return response.IsSuccessStatusCode ? await response.Content.ReadAsByteArrayAsync() : null;
        }

        public async Task<bool> DeleteBlobAsync(string container, string blob)
        {
            var response = await this._httpClient.DeleteAsync(StoragesEndpoint.DeleteBlobInContainer(container, blob));
            return response.IsSuccessStatusCode;
        }
    }
}