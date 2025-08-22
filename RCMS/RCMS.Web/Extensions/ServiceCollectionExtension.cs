using Microsoft.AspNetCore.Components.Authorization;
using RCMS.Web.Providers;
using RCMS.Web.Providers.Contracts;
using RCMS.Web.Services;
using RCMS.Web.Services.Contracts;

namespace RCMS.Web.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IHttpClientProvider, HttpClientProvider>();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }
}