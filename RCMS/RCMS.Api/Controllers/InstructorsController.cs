using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCMS.Application.Instructors.Commands;
using RCMS.Application.Instructors.Queries;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Models.Instructors;

namespace RCMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetInstructorsAsync() => await SendRequestAsync(new GetInstructorsQuery());
    
    [HttpGet("{id}"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetInstructorByIdAsync(Guid id) => await SendRequestAsync(new GetInstructorByIdQuery(id));
    
    [HttpPost, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> CreateInstructorAsync([FromBody] SaveInstructorDto request) => await SendRequestAsync(new CreateInstructorCommand(request));
    
    [HttpPut("{id}"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> UpdateInstructorAsync(Guid id, [FromBody] SaveInstructorDto request) => await SendRequestAsync(new UpdateInstructorCommand(id, request));
    
    [HttpDelete("{id}"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> DeleteInstructorAsync(Guid id) => await SendRequestAsync(new DeleteInstructorCommand(id));
    
    [HttpDelete, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> DeleteInstructorsAsync([FromBody] DeleteInstructorsDto request) => await SendRequestAsync(new DeleteInstructorsCommand(request));
}