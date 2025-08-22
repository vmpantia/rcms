using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RCMS.Shared.Models.Users;
using RCMS.Web.Providers;
using RCMS.Web.Providers.Contracts;
using RCMS.Web.Services.Contracts;

namespace RCMS.Web.Services;

public class AuthService(IHttpClientProvider httpClientProvider, AuthenticationStateProvider authenticationStateProvider, 
    NavigationManager navigationManager, ILogger<AuthService> logger) : IAuthService
{
    
    public async Task LoginAsync(LoginUserDto login)
    {
        try
        {
            // Send login request to API
            var token = await httpClientProvider.PostAsync<UserTokenDto>("https://localhost:7226/api/Auth/Login", login);

            // Check if the token is NOT NULL or Empty
            if (!string.IsNullOrEmpty(token.Token))
            {
                // Mark user as authenticate using the custom authentication state provider
                await ((CustomAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(token.Token);

                // Redirect user to the main page
                navigationManager.NavigateTo("/");
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in processing login request | Message: {ex.Message}");
            throw;
        }
    }

    public async Task LogoutAsync()
    {
        // Mark user as logged out using the custom authentication state provider
        await ((CustomAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();

        // Redirect user to the login page
        navigationManager.NavigateTo("/login");
    }
}