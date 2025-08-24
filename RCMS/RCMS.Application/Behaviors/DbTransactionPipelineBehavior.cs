using System.Transactions;
using MediatR;

namespace RCMS.Application.Behaviors;

public class DbTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Skip pipeline once the request name is not ends with Command
        if (!typeof(TRequest).Name.EndsWith("Command")) 
            return await next();

        // Create transaction scope
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        // Process the next pipeline
        var response = await next();

        // Complete all the transaction
        transactionScope.Complete();

        return response;
    }
}