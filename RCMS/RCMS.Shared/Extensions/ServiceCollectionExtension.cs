using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Validators;

namespace RCMS.Shared.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddValidators(this IServiceCollection services)
    {
        // Auto registration from assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Manual registration for using base validator
        services.AddScoped<IValidator<CreateStudentDto>, CreateStudentValidator>();
        services.AddScoped<IValidator<UpdateStudentDto>, UpdateStudentValidator>();
    }
}