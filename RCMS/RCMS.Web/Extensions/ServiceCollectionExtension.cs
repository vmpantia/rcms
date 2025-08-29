using Microsoft.AspNetCore.Components.Authorization;
using RCMS.Web.Helpers;
using RCMS.Web.Interfaces.Helpers;
using RCMS.Web.Interfaces.Providers;
using RCMS.Web.Interfaces.Services;
using RCMS.Web.Providers;
using RCMS.Web.Services;

namespace RCMS.Web.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<IDialogHelper, DialogHelper>();
    }

    public static void AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IHttpClientProvider, HttpClientProvider>();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IInstructorService, InstructorService>();
    }
}