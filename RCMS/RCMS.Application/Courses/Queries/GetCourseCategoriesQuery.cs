using AutoMapper;
using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Courses;
using RCMS.Shared.Responses;

namespace RCMS.Application.Courses.Queries;

public sealed record GetCourseCategoriesQuery(FilterCourseCategory Filter) : IRequest<Result<IEnumerable<CourseCategoryDto>>>;

public sealed class GetCourseCategoriesQueryHandler(ICourseCategoryRepository courseCategoryRepository, IMapper mapper) : IRequestHandler<GetCourseCategoriesQuery, Result<IEnumerable<CourseCategoryDto>>>
{
    public async Task<Result<IEnumerable<CourseCategoryDto>>> Handle(GetCourseCategoriesQuery request, CancellationToken cancellationToken)
    {
        // Filter data stored on the database
        var data = await courseCategoryRepository.FilterAsync(request.Filter, cancellationToken);
        
        // Map data to dto objects
        var result = mapper.Map<List<CourseCategoryDto>>(data);

        return result;
    }
}