using RCMS.Domain.Entities;
using RCMS.Shared.Models.Courses;

namespace RCMS.Domain.Interfaces.Repositories;

public interface ICourseCategoryRepository : IBaseRepository<CourseCategory>
{
    Task<IEnumerable<CourseCategory>> FilterAsync(FilterCourseCategory filter, CancellationToken cancellationToken = default);
}