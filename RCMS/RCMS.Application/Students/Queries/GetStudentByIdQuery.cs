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
        // Get data stored on the database using id
        var data = await studentRepository.Get(s => s.Id == request.Id)
            .Include(tbl => tbl.Enrollments)
            .FirstOrDefaultAsync(cancellationToken);
        
        // Check if data is NULL or not exist
        if (data is null) return StudentError.NotFound(request.Id);
        
        // Map data to dto object
        var result = mapper.Map<StudentDto>(data);
        
        return result;
    }
}