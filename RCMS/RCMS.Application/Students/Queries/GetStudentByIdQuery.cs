using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;

namespace RCMS.Application.Students.Queries;

public sealed record GetStudentByIdQuery(Guid Id) : IRequest<Result<StudentDto>>;

public sealed class GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
{
    public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        // Get student stored on the database using a student id
        var student = await studentRepository.Get(s => s.Id == request.Id)
            .Include(tbl => tbl.Enrollments)
            .FirstOrDefaultAsync(cancellationToken);
        
        // Check if a student is NULL or not exist
        if (student is null) return StudentError.NotFound(request.Id);
        
        // Map student to dto object
        var result = mapper.Map<StudentDto>(student);
        
        return result;
    }
}