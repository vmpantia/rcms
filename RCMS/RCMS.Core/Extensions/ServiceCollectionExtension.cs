using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RCMS.Core.Pipelines;
using RCMS.Core.Users;
using RCMS.Core.Users.Contracts;

namespace RCMS.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddMediatR();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }

    private static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationPipeline<,>));
            config.AddOpenBehavior(typeof(DbTransactionPipeline<,>));
        });
    }
}