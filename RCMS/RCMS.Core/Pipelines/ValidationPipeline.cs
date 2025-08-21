using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RCMS.Core.Pipelines;

public class ValidationPipeline<TRequest, TResponse>(IServiceProvider serviceProvider) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Skip pipeline once the request name is not ends with Command
        if (!typeof(TRequest).Name.EndsWith("Command")) 
            return await next();
        
        // Get validator service based on the TRequest
        var validator = serviceProvider.GetService<IValidator<TRequest>>();

        // Check if the validator is not null
        if (validator != null) await validator.ValidateAndThrowAsync(request, cancellationToken);

        return await next();
    }
}