using AuthService.Application.Common.Interfaces;

namespace AuthService.Application.Users.Commands.LoginUser;

public record LoginUserCommand : IRequest<LoginUserResult>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = default!;
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

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResult>
{
    private readonly IIdentityService _identityService;
    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginUserResult> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var (result, token, expiresIn) = await _identityService.LoginUserAsync(command.Email, command.Password, command.Role);

        return !result.Succeeded ? throw new UnauthorizedAccessException(result.Errors[0]) : new LoginUserResult(token, "Bearer", expiresIn);
    }
}
