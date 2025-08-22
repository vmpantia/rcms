using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCMS.Core.Students.Queries;
using RCMS.Shared.Enumerations;

namespace RCMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetStudentsAsync() => await SendRequestAsync(new GetStudentsQuery());
    
    [HttpGet("{id}"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetStudentByIdAsync(Guid id) => await SendRequestAsync(new GetStudentByIdQuery(id));
}