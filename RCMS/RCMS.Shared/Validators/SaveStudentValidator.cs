using FluentValidation;
using RCMS.Shared.Extensions;
using RCMS.Shared.Models.Students;

namespace RCMS.Shared.Validators;

public class SaveStudentValidator : AbstractValidator<SaveStudentDto>
{
    public SaveStudentValidator()
    {
        RuleFor(csd => csd.FirstName)
            .NotEmpty()
            .WithMessage("First Name is required.");
        RuleFor(csd => csd.LastName)
            .NotEmpty()
            .WithMessage("Last Name is required.");
        RuleFor(csd => csd.Gender)
            .NotEmpty()
            .WithMessage("Gender is required.");
        RuleFor(csd => csd.BirthDate)
            .NotEmpty()
            .WithMessage("Birthday is required.")
            .Must(value => value?.Date < DateTime.Today)
            .WithMessage("Birthdate must be less than today.")
            .Must(value => value.GetAge() >= Constant.MINIMUM_AGE)
            .WithMessage($"Student must be at least {Constant.MINIMUM_AGE} years old.");
        RuleFor(csd => csd.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Must(value => decimal.TryParse(value, out _))
            .WithMessage("Phone number must be a number.")
            .Length(Constant.PHONE_NUMBER_LENGTH)
            .WithMessage("Phone number must be 11 digits long.");
        RuleFor(csd => csd.EmailAddress)
            .NotEmpty()
            .WithMessage("Email address is required.")
            .EmailAddress()
            .WithMessage("Email address it not a valid email address.");
    }
}

public sealed class CreateStudentValidator : SaveStudentValidator;

public sealed class UpdateStudentValidator : SaveStudentValidator;