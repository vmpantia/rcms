using FluentValidation;
using MediatR;
using RCMS.Domain.Interfaces.Authentication;
using RCMS.Domain.Interfaces.Repositories;
using RCMS.Shared.Models.Users;
using RCMS.Shared.Responses;
using RCMS.Shared.Responses.Errors;
using RCMS.Shared.Validators;

namespace RCMS.Application.Users.Commands;

public sealed record LoginUserCommand(LoginUserDto Login) : IRequest<Result<UserTokenDto>>;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(luc => luc.Login)
            .SetValidator(new LoginUserValidator());
    }
}

public sealed class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider) 
    : IRequestHandler<LoginUserCommand, Result<UserTokenDto>>
{
    public async Task<Result<UserTokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Get user stored on the database using a username or email address
        var user = await userRepository.GetOneAsync(expression: u =>
                u.Username == request.Login.UsernameOrEmailAddress ||
                u.EmailAddress == request.Login.UsernameOrEmailAddress,
            cancellationToken);

        // Check if user NULL or not exist
        if (user == null) return UserError.UsernameOrEmailAddressNotFound();
        
        // Check if user password matches on the given password using password hasher
        if (!passwordHasher.Verify(request.Login.Password, user.Password)) return UserError.PasswordIncorrect();

        // Generate authentication token for user
        return tokenProvider.Create(user);
    }
}