using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;

namespace RCMS.Application.Students.Commands;

public sealed record DeleteStudentCommand(Guid Id) : IRequest<Result<Guid>>;

public sealed class DeleteStudentCommandHandler(IStudentRepository studentRepository) : IRequestHandler<DeleteStudentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        // Get data stored on the database using id
        var dataToDelete = await studentRepository.GetOneAsync(s => s.Id == request.Id, cancellationToken);
        
        // Check if data is NULL or not exist
        if (dataToDelete is null) return StudentError.NotFound(request.Id);
        
        // Delete data on the database
        await studentRepository.DeleteAsync(dataToDelete, cancellationToken);

        return dataToDelete.Id;
    }
}