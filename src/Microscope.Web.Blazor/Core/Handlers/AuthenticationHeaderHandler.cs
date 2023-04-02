using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Microscope.Admin.Core.Handlers
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly IAccessTokenProvider accessTokenProvider;

        public AuthenticationHeaderHandler(IAccessTokenProvider accessTokenProvider)
            => this.accessTokenProvider = accessTokenProvider;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != "Bearer")
            {
                var accessTokenResult = await accessTokenProvider.RequestAccessToken();
                if (accessTokenResult.TryGetToken(out var token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}