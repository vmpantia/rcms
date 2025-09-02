using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Courses;
using RCMS.Shared.Responses;

namespace RCMS.Application.Courses.Queries;

public sealed class GetCoursesQuery : IRequest<Result<IEnumerable<CourseDto>>>;

public sealed class GetCoursesQueryHandler(ICourseRepository courseRepository, IMapper mapper) : IRequestHandler<GetCoursesQuery, Result<IEnumerable<CourseDto>>>
{
    public async Task<Result<IEnumerable<CourseDto>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        // Get data stored on the database
        var data = await courseRepository.Get()
            .Include(tbl => tbl.Category)
            .OrderByDescending(cc => cc.CreatedAt)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
        
        // Map entity to lite objects
        var result = mapper.Map<List<CourseDto>>(data);

        return result;
    }
}