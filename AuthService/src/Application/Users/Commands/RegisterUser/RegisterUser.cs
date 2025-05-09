using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Constants;

namespace AuthService.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand : IRequest<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        
        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role must be provided.")
            .Must(role => role.Equals(Roles.Customer) || role.Equals(Roles.Organizer))
            .WithMessage("Invalid role name");
    }
}

public class RegisterUserCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (result, userId) = await identityService.CreateUserAsync(request.Email, request.Password, request.Role);

        return !result.Succeeded ? result.Errors[0] : "";
    }
}
