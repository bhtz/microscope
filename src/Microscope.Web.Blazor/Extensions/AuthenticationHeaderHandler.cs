using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Microscope.Web.Blazor.Extensions;

public class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider accessTokenProvider;
    private readonly NavigationManager _navigationManager;

    public AuthenticationHeaderHandler(IAccessTokenProvider accessTokenProvider, NavigationManager navigationManager)
    {
        this.accessTokenProvider = accessTokenProvider;
        this._navigationManager = navigationManager;
    }

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

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("/authentication/login", forceLoad: true);
        }

        return response;
    }
}