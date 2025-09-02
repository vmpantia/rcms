using AutoMapper;
using FluentValidation;
using MediatR;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;
using RCMS.Shared.Validators;

namespace RCMS.Application.Students.Commands;

public sealed record CreateStudentCommand(SaveStudentDto Student) : IRequest<Result<Guid>>;

public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator(IStudentRepository studentRepository)
    {
        RuleFor(csc => csc.Student)
            .SetValidator(new SaveStudentValidator());

        RuleFor(csc => csc.Student)
            .MustAsync(async (ssd, ct) =>
            {
                // Check if the data already exists on the database
                var result = await studentRepository.IsExistAsync(
                    expression: s => s.FirstName == ssd.FirstName && s.LastName == ssd.LastName,
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
        // Map data to entity
        var entity = mapper.Map<Student>(request.Student);

        // Create data on the database
        var result = await studentRepository.CreateAsync(entity, cancellationToken);

        return result.Id;
    }
}