using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microscope.SDK.Dotnet.Routes;
using System;
using Microscope.Application.Features.Analytic.Commands;
using Microscope.Application.Features.Analytic.Queries;

namespace Microscope.SDK.Dotnet
{
    public partial class MicroscopeClient
    {
        public async Task<string> PostAnalyticAsync(AddAnalyticCommand command)
        {
            var response = await this._httpClient.PostAsJsonAsync(AnalyticsEndpoint.Create, command);
            var clientId = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
            return clientId;
        }

        public async Task<bool> PutAnalyticAsync(Guid id, EditAnalyticCommand command)
        {
            var response = await this._httpClient.PutAsJsonAsync(AnalyticsEndpoint.Update(id), command);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<AnalyticQueryResult>> GetAnalyticsAsync()
        {
            return await this._httpClient.GetFromJsonAsync<IEnumerable<AnalyticQueryResult>>(AnalyticsEndpoint.GetAll);
        }
    }
}