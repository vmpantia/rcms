using FluentValidation;
using RCMS.Shared.Models.Users;

namespace RCMS.Shared.Validators;

public sealed class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(lud => lud.UsernameOrEmailAddress).NotEmpty().WithMessage("Invalid username or email.");
        RuleFor(lud => lud.Password).NotEmpty().WithMessage("Invalid password.");
    }
}