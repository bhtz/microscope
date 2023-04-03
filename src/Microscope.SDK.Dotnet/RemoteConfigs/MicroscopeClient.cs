using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microscope.SDK.Dotnet.Routes;
using System;
using Microscope.Core.Queries.RemoteConfig;
using Microscope.Features.RemoteConfig.Commands;

namespace Microscope.SDK.Dotnet
{
    public partial class MicroscopeClient
    {
        public async Task<string> PostRemoteConfigAsync(AddRemoteConfigCommand command)
        {

            var response = await this._httpClient.PostAsJsonAsync(RemoteConfigsEndpoint.Create, command);
            var clientId = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
            return clientId;
        }

        public async Task<bool> PutRemoteConfigAsync(Guid id, EditRemoteConfigCommand remoteConfig)
        {
            var response = await this._httpClient.PutAsJsonAsync(RemoteConfigsEndpoint.Update(id), remoteConfig);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<FilteredRemoteConfigQueryResult>> GetRemoteConfigsAsync()
        {
            return await this._httpClient.GetFromJsonAsync<IEnumerable<FilteredRemoteConfigQueryResult>>(RemoteConfigsEndpoint.GetAll);
        }
    }
}