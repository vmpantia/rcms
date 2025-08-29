using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Instructors;
using RCMS.Shared.Responses;

namespace RCMS.Application.Instructors.Commands;

public sealed record DeleteInstructorsCommand(DeleteInstructorsDto Instructors) : IRequest<Result<IEnumerable<Guid>>>;

public sealed class DeleteInstructorsCommandHandler(IInstructorRepository instructorRepository) : IRequestHandler<DeleteInstructorsCommand, Result<IEnumerable<Guid>>>
{
    public async Task<Result<IEnumerable<Guid>>> Handle(DeleteInstructorsCommand request, CancellationToken cancellationToken)
    {
        // Delete data on the database
        await instructorRepository.DeleteAsync(request.Instructors.Ids, cancellationToken);

        return request.Instructors.Ids.ToList();
    }
}