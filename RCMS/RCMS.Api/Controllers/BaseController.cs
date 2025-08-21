using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Extensions;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;

namespace RCMS.Api.Controllers;

public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;
    protected BaseController(IMediator mediator) => _mediator = mediator;
    
    protected async Task<IActionResult> SendRequestAsync<TResponse>(IRequest<Result<TResponse>> request) where TResponse : class
    {
        try
        {
            // Send a request to command or query
            var result = await _mediator.Send(request);
            
            return result switch
            {
                { IsSuccess: false, Error: { Type: ErrorType.NotFound } } => NotFound(result),
                { IsSuccess: false } => BadRequest(result),
                _ => Ok(result)
            };
        }
        catch (ValidationException ex)
        {
            var error = DefaultError.Validation(ex.Errors.ToDictionary());
            return BadRequest((Result<TResponse>)error);
        }
        catch (Exception ex)
        {
            return BadRequest((Result<TResponse>)DefaultError.Unexpected(ex));
        }
    }
}