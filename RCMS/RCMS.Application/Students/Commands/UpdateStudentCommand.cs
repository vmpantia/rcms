using AutoMapper;
using FluentValidation;
using MediatR;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Students;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;
using RCMS.Shared.Validators;

namespace RCMS.Application.Students.Commands;

public sealed record UpdateStudentCommand(Guid StudentId, UpdateStudentDto Student) : IRequest<Result<Guid>>;

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator(IStudentRepository studentRepository)
    {
        RuleFor(usd => usd.Student)
            .SetValidator(new UpdateStudentValidator());

        RuleFor(usd => usd)
            .CustomAsync(async (usd, context, ct) =>
            {
                // Get student stored on the database using a student id
                var studentToUpdate = await studentRepository.GetOneAsync(s => s.Id == usd.StudentId, ct);
                
                // Check if a student exists
                if (studentToUpdate is null)
                {
                    context.AddFailure("Student is not exists.");
                    return;
                }

                // Check if any changes made on the student
                if (usd.Student.FirstName == studentToUpdate.FirstName &&
                    usd.Student.MiddleName == studentToUpdate.MiddleName &&
                    usd.Student.LastName == studentToUpdate.LastName &&
                    usd.Student.Gender == studentToUpdate.Gender.ToString() &&
                    usd.Student.BirthDate == studentToUpdate.BirthDate &&
                    usd.Student.PhoneNumber == studentToUpdate.PhoneNumber &&
                    usd.Student.EmailAddress == studentToUpdate.EmailAddress)
                    context.AddFailure("No changes made on the student.");
            });

        RuleFor(usd => usd)
            .MustAsync(async (usd, ct) =>
            {
                // Check if the student already exists on the database by checking first name and last name with different id
                var result = await studentRepository.IsExistAsync(
                    expression: s => s.Id != usd.StudentId && 
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
        // Get student stored on the database using a student id
        var studentToUpdate = await studentRepository.GetOneAsync(s => s.Id == request.StudentId, cancellationToken);
        
        // Check if a student is NULL or not exist
        if (studentToUpdate is null) return StudentError.NotFound(request.StudentId);
        
        // Map student to entity
        var updatedEntity = mapper.Map(request.Student, studentToUpdate);
        
        // Update student on the database
        var result = await studentRepository.UpdateAsync(updatedEntity, cancellationToken);

        return result.Id;
    }
}