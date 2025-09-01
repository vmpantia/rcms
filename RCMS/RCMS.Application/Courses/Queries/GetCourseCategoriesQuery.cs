using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Courses;
using RCMS.Shared.Responses;

namespace RCMS.Application.Courses.Queries;

public sealed class GetCourseCategoriesQuery : IRequest<Result<IEnumerable<CourseCategoryDto>>>;

public sealed class GetCourseCategoriesQueryHandler(ICourseCategoryRepository courseCategoryRepository, IMapper mapper) : IRequestHandler<GetCourseCategoriesQuery, Result<IEnumerable<CourseCategoryDto>>>
{
    public async Task<Result<IEnumerable<CourseCategoryDto>>> Handle(GetCourseCategoriesQuery request, CancellationToken cancellationToken)
    {
        // Get data stored on the database
        var data = await courseCategoryRepository.Get()
            .Include(tbl => tbl.Courses)
            .OrderByDescending(cc => cc.CreatedAt)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
        
        // Map data to dto objects
        var result = mapper.Map<List<CourseCategoryDto>>(data);

        return result;
    }
}