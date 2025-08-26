using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;

namespace RCMS.Application.Students.Commands;

public sealed record DeleteStudentsCommand(DeleteStudentsDto Students) : IRequest<Result<IEnumerable<Guid>>>;

public sealed class DeleteStudentsCommandHandler(IStudentRepository studentRepository) : IRequestHandler<DeleteStudentsCommand, Result<IEnumerable<Guid>>>
{
    public async Task<Result<IEnumerable<Guid>>> Handle(DeleteStudentsCommand request, CancellationToken cancellationToken)
    {
        // Delete students on the database
        await studentRepository.DeleteAsync(request.Students.Ids, cancellationToken);

        return request.Students.Ids.ToList();
    }
}