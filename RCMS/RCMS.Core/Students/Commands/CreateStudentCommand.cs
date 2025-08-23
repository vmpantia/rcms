using AutoMapper;
using FluentValidation;
using MediatR;
using RCMS.Infrastructure.DataAccess.Entities;
using RCMS.Infrastructure.DataAccess.Repositories.Contracts;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;
using RCMS.Shared.Validators;

namespace RCMS.Core.Students.Commands;

public sealed record CreateStudentCommand(CreateStudentDto Student) : IRequest<Result<Guid>>;

public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator(IStudentRepository studentRepository)
    {
        RuleFor(csc => csc.Student)
            .SetValidator(new CreateStudentValidator());

        RuleFor(csc => csc.Student)
            .MustAsync(async (csd, ct) =>
            {
                // Check if the student already exists on the database by checking first name and last name
                var result = await studentRepository.IsExistAsync(
                    expression: s => s.FirstName == csd.FirstName && s.LastName == csd.LastName,
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("Student first name and last name is already exist in the database.");
    }
}

public sealed class CreateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<CreateStudentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        // Map student to entity
        var entity = mapper.Map<Student>(request.Student);

        // Create student on the database
        var result = await studentRepository.CreateAsync(entity, cancellationToken);

        return result.Id;
    }
}