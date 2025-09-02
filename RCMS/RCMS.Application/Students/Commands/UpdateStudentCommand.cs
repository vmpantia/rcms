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
        RuleFor(usc => usc.Student)
            .SetValidator(new SaveStudentValidator());

        RuleFor(usc => usc)
            .CustomAsync(async (usc, context, ct) =>
            {
                // Get data stored on the database using id
                var dataToUpdate = await studentRepository.GetOneAsync(s => s.Id == usc.Id, ct);
                
                // Check if data exists
                if (dataToUpdate is null)
                {
                    context.AddFailure("Student is not exists.");
                    return;
                }

                // Check if any changes made on the student
                if (usc.Student.FirstName == dataToUpdate.FirstName &&
                    usc.Student.MiddleName == dataToUpdate.MiddleName &&
                    usc.Student.LastName == dataToUpdate.LastName &&
                    usc.Student.Gender == dataToUpdate.Gender.ToString() &&
                    usc.Student.BirthDate == dataToUpdate.BirthDate &&
                    usc.Student.PhoneNumber == dataToUpdate.PhoneNumber &&
                    usc.Student.EmailAddress == dataToUpdate.EmailAddress)
                    context.AddFailure("No changes made on the student.");
            });

        RuleFor(usc => usc)
            .MustAsync(async (usc, ct) =>
            {
                // Check if the student already exists on the database by checking first name and last name with different id
                var result = await studentRepository.IsExistAsync(
                    expression: s => s.Id != usc.Id && 
                                     s.FirstName == usc.Student.FirstName && 
                                     s.LastName == usc.Student.LastName,
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