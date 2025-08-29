using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Instructors;
using RCMS.Shared.Responses;

namespace RCMS.Application.Instructors.Queries;

public sealed class GetInstructorsQuery : IRequest<Result<IEnumerable<InstructorLiteDto>>>;

public sealed class GetInstructorsQueryHandler(IInstructorRepository instructorRepository, IMapper mapper) : IRequestHandler<GetInstructorsQuery, Result<IEnumerable<InstructorLiteDto>>>
{
    public async Task<Result<IEnumerable<InstructorLiteDto>>> Handle(GetInstructorsQuery request, CancellationToken cancellationToken)
    {
        // Get data stored on the database
        var data = await instructorRepository.Get()
            .OrderByDescending(tbl => tbl.CreatedAt)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
        
        // Map data to lite objects
        var result = mapper.Map<List<InstructorLiteDto>>(data);

        return result;
    }
}