using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCMS.Application.Students.Commands;
using RCMS.Application.Students.Queries;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Models.Students;

namespace RCMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("Filter"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetStudentsAsync([FromBody] FilterStudent filter) => await SendRequestAsync(new GetStudentsQuery(filter));
    
    [HttpGet("{id}"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetStudentByIdAsync(Guid id) => await SendRequestAsync(new GetStudentByIdQuery(id));
    
    [HttpPost, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> CreateStudentAsync([FromBody] SaveStudentDto request) => await SendRequestAsync(new CreateStudentCommand(request));
    
    [HttpPut("{id}"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> UpdateStudentAsync(Guid id, [FromBody] SaveStudentDto request) => await SendRequestAsync(new UpdateStudentCommand(id, request));
    
    [HttpDelete("{id}"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> DeleteStudentAsync(Guid id) => await SendRequestAsync(new DeleteStudentCommand(id));
    
    [HttpDelete, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> DeleteStudentsAsync([FromBody] DeleteStudentsDto request) => await SendRequestAsync(new DeleteStudentsCommand(request));
}