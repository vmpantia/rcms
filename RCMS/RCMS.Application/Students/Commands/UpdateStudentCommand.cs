using AutoMapper;
using FluentValidation;
using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;
using RCMS.Shared.Validators;

namespace RCMS.Application.Students.Commands;

public sealed record UpdateStudentCommand(Guid Id, SaveStudentDto Student) : IRequest<Result<Guid>>;

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator(IStudentRepository studentRepository)
    {
        RuleFor(usd => usd.Student)
            .SetValidator(new SaveStudentValidator());

        RuleFor(usd => usd)
            .CustomAsync(async (usd, context, ct) =>
            {
                // Get data stored on the database using id
                var dataToUpdate = await studentRepository.GetOneAsync(s => s.Id == usd.Id, ct);
                
                // Check if data exists
                if (dataToUpdate is null)
                {
                    context.AddFailure("Student is not exists.");
                    return;
                }

                // Check if any changes made on the student
                if (usd.Student.FirstName == dataToUpdate.FirstName &&
                    usd.Student.MiddleName == dataToUpdate.MiddleName &&
                    usd.Student.LastName == dataToUpdate.LastName &&
                    usd.Student.Gender == dataToUpdate.Gender.ToString() &&
                    usd.Student.BirthDate == dataToUpdate.BirthDate &&
                    usd.Student.PhoneNumber == dataToUpdate.PhoneNumber &&
                    usd.Student.EmailAddress == dataToUpdate.EmailAddress)
                    context.AddFailure("No changes made on the student.");
            });

        RuleFor(usd => usd)
            .MustAsync(async (usd, ct) =>
            {
                // Check if the student already exists on the database by checking first name and last name with different id
                var result = await studentRepository.IsExistAsync(
                    expression: s => s.Id != usd.Id && 
                                     s.FirstName == usd.Student.FirstName && 
                                     s.LastName == usd.Student.LastName,
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("Student first name and last name is already exist in the database.");
    }
}

public sealed class UpdateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper)  : IRequestHandler<UpdateStudentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        // Get data stored on the database using id
        var dataToUpdate = await studentRepository.GetOneAsync(s => s.Id == request.Id, cancellationToken);
        
        // Check if data is NULL or not exist
        if (dataToUpdate is null) return StudentError.NotFound(request.Id);
        
        // Map data to entity
        var updatedEntity = mapper.Map(request.Student, dataToUpdate);
        
        // Update data on the database
        var result = await studentRepository.UpdateAsync(updatedEntity, cancellationToken);

        return result.Id;
    }
}