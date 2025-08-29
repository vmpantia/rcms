using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Infrastructure.DataAccess.Contexts;
using RCMS.Infrastructure.DataAccess.Interceptors;
using RCMS.Infrastructure.DataAccess.Repositories;

namespace RCMS.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInterceptors();
        services.AddDbContexts(configuration);
        services.AddRepositories();
    }

    private static void AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<AuditEntitiesInterceptor>();
    }

    private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RCMSDbContext>((sp, opt) =>
        {
            var interceptor = sp.GetRequiredService<AuditEntitiesInterceptor>();

            opt.UseSqlServer(configuration.GetConnectionString("MigrationDb"))
                .AddInterceptors(interceptor);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}