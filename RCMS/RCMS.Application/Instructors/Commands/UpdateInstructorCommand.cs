using AutoMapper;
using FluentValidation;
using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Instructors;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;
using RCMS.Shared.Validators;

namespace RCMS.Application.Instructors.Commands;

public sealed record UpdateInstructorCommand(Guid Id, SaveInstructorDto Instructor) : IRequest<Result<Guid>>;

public sealed class UpdateInstructorCommandValidator : AbstractValidator<UpdateInstructorCommand>
{
    public UpdateInstructorCommandValidator(IInstructorRepository instructorRepository)
    {
        RuleFor(usd => usd.Instructor)
            .SetValidator(new SaveInstructorValidator());

        RuleFor(usd => usd)
            .CustomAsync(async (usd, context, ct) =>
            {
                // Get data stored on the database using an id
                var dataToUpdate = await instructorRepository.GetOneAsync(s => s.Id == usd.Id, ct);
                
                // Check if data exists
                if (dataToUpdate is null)
                {
                    context.AddFailure("Instructor is not exists.");
                    return;
                }

                // Check if any changes made on the data
                if (usd.Instructor.FirstName == dataToUpdate.FirstName &&
                    usd.Instructor.MiddleName == dataToUpdate.MiddleName &&
                    usd.Instructor.LastName == dataToUpdate.LastName &&
                    usd.Instructor.Gender == dataToUpdate.Gender.ToString() &&
                    usd.Instructor.BirthDate == dataToUpdate.BirthDate &&
                    usd.Instructor.PhoneNumber == dataToUpdate.PhoneNumber &&
                    usd.Instructor.EmailAddress == dataToUpdate.EmailAddress)
                    context.AddFailure("No changes made on the instructor.");
            });

        RuleFor(usd => usd)
            .MustAsync(async (usd, ct) =>
            {
                // Check if the data already exists on the database
                var result = await instructorRepository.IsExistAsync(
                    expression: s => s.Id != usd.Id && 
                                     s.FirstName == usd.Instructor.FirstName && 
                                     s.LastName == usd.Instructor.LastName,
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("Instructor first name and last name is already exist in the database.");
    }
}

public sealed class UpdateInstructorCommandHandler(IInstructorRepository instructorRepository, IMapper mapper)  : IRequestHandler<UpdateInstructorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
    {
        // Get data stored on the database using id
        var dataToUpdate = await instructorRepository.GetOneAsync(s => s.Id == request.Id, cancellationToken);
        
        // Check if data is NULL or not exist
        if (dataToUpdate is null) return InstructorError.NotFound(request.Id);
        
        // Map data to entity
        var updatedEntity = mapper.Map(request.Instructor, dataToUpdate);
        
        // Update data on the database
        var result = await instructorRepository.UpdateAsync(updatedEntity, cancellationToken);

        return result.Id;
    }
}