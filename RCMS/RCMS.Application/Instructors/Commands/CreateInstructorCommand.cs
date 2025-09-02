using AutoMapper;
using FluentValidation;
using MediatR;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Instructors;
using RCMS.Shared.Responses;
using RCMS.Shared.Validators;

namespace RCMS.Application.Instructors.Commands;

public sealed record CreateInstructorCommand(SaveInstructorDto Instructor) : IRequest<Result<Guid>>;

public sealed class CreateInstructorCommandValidator : AbstractValidator<CreateInstructorCommand>
{
    public CreateInstructorCommandValidator(IInstructorRepository instructorRepository)
    {
        RuleFor(cic => cic.Instructor)
            .SetValidator(new SaveInstructorValidator());

        RuleFor(cic => cic.Instructor)
            .MustAsync(async (sid, ct) =>
            {
                // Check if the data already exists on the database
                var result = await instructorRepository.IsExistAsync(
                    expression: i => i.FirstName == sid.FirstName && i.LastName == sid.LastName,
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("Instructor first name and last name is already exist in the database.");
    }
}

public sealed class CreateInstructorCommandHandler(IInstructorRepository instructorRepository, IMapper mapper) : IRequestHandler<CreateInstructorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
    {
        // Map data to entity
        var entity = mapper.Map<Instructor>(request.Instructor);

        // Create data on the database
        var result = await instructorRepository.CreateAsync(entity, cancellationToken);

        return result.Id;
    }
}