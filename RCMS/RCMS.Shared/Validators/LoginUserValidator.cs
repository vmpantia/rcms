using FluentValidation;
using RCMS.Shared.Models.Users;

namespace RCMS.Shared.Validators;

public sealed class LoginUserValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(e => e.UsernameOrEmailAddress).NotEmpty().WithMessage("Invalid username or email.");
        RuleFor(e => e.Password).NotEmpty().WithMessage("Invalid password.");
    }
}