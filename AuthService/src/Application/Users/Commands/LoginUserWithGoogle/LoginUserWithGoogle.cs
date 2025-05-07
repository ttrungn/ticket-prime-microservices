using AuthService.Application.Common.Interfaces;
using AuthService.Application.Common.Models;
using AuthService.Application.Users.Commands.LoginUser;
using AuthService.Domain.Constants;

namespace AuthService.Application.Users.Commands.LoginUserWithGoogle;

public record LoginUserWithGoogleCommand : IRequest<LoginUserResult>
{
    public string Code { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;
    public string Role { get; set; } = null!;
}

public class LoginUserWithGoogleCommandValidator : AbstractValidator<LoginUserWithGoogleCommand>
{
    public LoginUserWithGoogleCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Authorization code must be provided.");

        RuleFor(x => x.RedirectUri)
            .NotEmpty()
            .WithMessage("Redirect URI must be provided.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Redirect URI must be a valid absolute URI.");

        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role must be provided.")
            .Must(role => role.Equals(Roles.Customer) || role.Equals(Roles.Organizer))
            .WithMessage("Invalid role name");
    }
}

public class LoginUserWithGoogleCommandHandler : IRequestHandler<LoginUserWithGoogleCommand, LoginUserResult?>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IGoogleService _googleService;

    public LoginUserWithGoogleCommandHandler(IApplicationDbContext context, IIdentityService identityService, IGoogleService googleService)
    {
        _context = context;
        _identityService = identityService;
        _googleService = googleService;
    }

    public async Task<LoginUserResult?> Handle(LoginUserWithGoogleCommand request, CancellationToken cancellationToken)
    {
        (string providerKey, string email) = await _googleService.ExchangeCodeAsync(request.Code, request.RedirectUri);
        
        (Result result, string token, string tokenType, int expiresIn) = await _identityService.LoginGoogleUserAsync(email, providerKey, request.Role);
        return !result.Succeeded ? null : new LoginUserResult(token, tokenType, expiresIn);
    }
}
