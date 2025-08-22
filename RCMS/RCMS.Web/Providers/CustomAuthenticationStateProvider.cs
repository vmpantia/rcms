using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace RCMS.Web.Providers;

public class CustomAuthenticationStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
{
    private const string AuthTokenKey = "authToken";
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetItemAsync<string>(AuthTokenKey);
        var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : GetClaimsIdentity(token);
        var user = new ClaimsPrincipal(identity);   
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await localStorage.SetItemAsync(AuthTokenKey, token);
        var identity = GetClaimsIdentity(token);
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await localStorage.RemoveItemAsync(AuthTokenKey);
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private ClaimsIdentity GetClaimsIdentity(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return new ClaimsIdentity(jwtToken.Claims, "jwt");
    }
}