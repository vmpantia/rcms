using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RCMS.Infrastructure.DataAccess.Repositories.Contracts;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;

namespace RCMS.Core.Students.Queries;

public sealed class GetStudentsQuery : IRequest<Result<IEnumerable<StudentLiteDto>>>;

public sealed class GetStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<GetStudentsQuery, Result<IEnumerable<StudentLiteDto>>>
{
    public async Task<Result<IEnumerable<StudentLiteDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        // Get students stored on the database
        var students = await studentRepository.Get()
            .Include(tbl => tbl.Enrollments)
            .OrderByDescending(tbl => tbl.CreatedAt)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
        
        // Map students to lite objects
        var result = mapper.Map<List<StudentLiteDto>>(students);

        return result;
    }
}