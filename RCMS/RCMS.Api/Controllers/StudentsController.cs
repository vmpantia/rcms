using MediatR;
using Microsoft.AspNetCore.Mvc;
using RCMS.Core.Students.Queries;

namespace RCMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetStudentsAsync() => await SendRequestAsync(new GetStudentsQuery());
    
    [HttpGet("{id}")] 
    public async Task<IActionResult> GetStudentByIdAsync(Guid id) => await SendRequestAsync(new GetStudentByIdQuery(id));
}