using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Instructors;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;

namespace RCMS.Application.Instructors.Queries;

public sealed record GetInstructorByIdQuery(Guid Id) : IRequest<Result<InstructorDto>>;

public sealed class GetInstructorByIdQueryHandler(IInstructorRepository instructorRepository, IMapper mapper) : IRequestHandler<GetInstructorByIdQuery, Result<InstructorDto>>
{
    public async Task<Result<InstructorDto>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
    {
        // Get data stored on the database using id
        var data = await instructorRepository.Get(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        // Check if data is NULL or not exist
        if (data is null) return InstructorError.NotFound(request.Id);
        
        // Map data to a dto object
        var result = mapper.Map<InstructorDto>(data);
        
        return result;
    }
}