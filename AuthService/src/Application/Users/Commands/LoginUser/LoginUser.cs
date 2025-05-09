using AuthService.Application.Common.Interfaces;
using AuthService.Application.Common.Models;

namespace AuthService.Application.Users.Commands.LoginUser;

public record LoginUserCommand : IRequest<LoginUserResult>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}

public record LoginUserResult(string AccessToken, string TokenType, int ExpiresIn);

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}

public class LoginUserCommandHandler(IIdentityService identityService)
    : IRequestHandler<LoginUserCommand, LoginUserResult?>
{
    public async Task<LoginUserResult?> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        (Result result, string token, string tokenType, int expiresIn) = await identityService.LoginUserAsync(command.Email, command.Password, command.Role);
        return !result.Succeeded ? null : new LoginUserResult(token, tokenType, expiresIn);
    }
}
