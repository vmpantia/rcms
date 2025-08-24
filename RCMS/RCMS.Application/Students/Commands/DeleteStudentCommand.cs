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
        // Get student stored on the database using a student id
        var studentToDelete = await studentRepository.GetOneAsync(s => s.Id == request.Id, cancellationToken);
        
        // Check if a student is NULL or not exist
        if (studentToDelete is null) return StudentError.NotFound(request.Id);
        
        // Delete student on the database
        await studentRepository.DeleteAsync(studentToDelete, cancellationToken);

        return studentToDelete.Id;
    }
}