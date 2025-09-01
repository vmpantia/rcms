using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCMS.Application.Courses.Queries;
using RCMS.Shared.Enumerations;

namespace RCMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("Categories"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetCourseCategoriesAsync() => await SendRequestAsync(new GetCourseCategoriesQuery());
}