using FluentValidation;
using RCMS.Shared.Extensions;
using RCMS.Shared.Models.Instructors;

namespace RCMS.Shared.Validators;

public class SaveInstructorValidator : AbstractValidator<SaveInstructorDto>
{
    public SaveInstructorValidator()
    {
        RuleFor(sid => sid.FirstName)
            .NotEmpty()
            .WithMessage("First Name is required.");
        RuleFor(sid => sid.LastName)
            .NotEmpty()
            .WithMessage("Last Name is required.");
        RuleFor(sid => sid.Gender)
            .NotEmpty()
            .WithMessage("Gender is required.");
        RuleFor(sid => sid.BirthDate)
            .NotEmpty()
            .WithMessage("Birthday is required.")
            .Must(value => value?.Date < DateTime.Today)
            .WithMessage("Birthdate must be less than today.")
            .Must(value => value.GetAge() >= Constant.MINIMUM_AGE)
            .WithMessage($"Instructor must be at least {Constant.MINIMUM_AGE} years old.");
        RuleFor(sid => sid.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Must(value => decimal.TryParse(value, out _))
            .WithMessage("Phone number must be a number.")
            .Length(Constant.PHONE_NUMBER_LENGTH)
            .WithMessage("Phone number must be 11 digits long.");
        RuleFor(sid => sid.EmailAddress)
            .NotEmpty()
            .WithMessage("Email address is required.")
            .EmailAddress()
            .WithMessage("Email address it not a valid email address.");
    }
}