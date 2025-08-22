using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RCMS.Shared.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddSharedValidators(this IServiceCollection services) =>
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}