using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;

namespace RCMS.Application.Instructors.Commands;

public sealed record DeleteInstructorCommand(Guid Id) : IRequest<Result<Guid>>;

public sealed class DeleteInstructorCommandHandler(IInstructorRepository instructorRepository) : IRequestHandler<DeleteInstructorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
    {
        // Get data stored on the database using an id
        var dataToDelete = await instructorRepository.GetOneAsync(s => s.Id == request.Id, cancellationToken);
        
        // Check if data is NULL or not exist
        if (dataToDelete is null) return InstructorError.NotFound(request.Id);
        
        // Delete data on the database
        await instructorRepository.DeleteAsync(dataToDelete, cancellationToken);

        return dataToDelete.Id;
    }
}