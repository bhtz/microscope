using System;
using System.Net.Http;

namespace Microscope.SDK.Dotnet
{
    public partial class MicroscopeClient
    {
        private readonly HttpClient _httpClient;
        
        public MicroscopeClient(HttpClient client)
        {
            _httpClient = client;
        }
    }
}
