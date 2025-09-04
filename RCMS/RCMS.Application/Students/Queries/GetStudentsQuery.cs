using AutoMapper;
using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;

namespace RCMS.Application.Students.Queries;

public sealed record GetStudentsQuery(FilterStudent Filter) : IRequest<Result<IEnumerable<StudentLiteDto>>>;

public sealed class GetStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<GetStudentsQuery, Result<IEnumerable<StudentLiteDto>>>
{
    public async Task<Result<IEnumerable<StudentLiteDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        // Get data stored on the database
        var data = await studentRepository.FilterAsync(request.Filter, cancellationToken);
        
        // Map data to lite objects
        var result = mapper.Map<List<StudentLiteDto>>(data);

        return result;
    }
}