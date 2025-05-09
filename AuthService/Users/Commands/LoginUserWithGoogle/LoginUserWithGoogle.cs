using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Users.Commands.LoginUserWithGoogle;

public record LoginUserWithGoogleCommand : IRequest<LoginUserResult>
{
}

public class LoginUserWithGoogleCommandValidator : AbstractValidator<LoginUserWithGoogleCommand>
{
    public LoginUserWithGoogleCommandValidator()
    {
    }
}

public class LoginUserWithGoogleCommandHandler : IRequestHandler<LoginUserWithGoogleCommand, LoginUserResult>
{
    private readonly IApplicationDbContext _context;

    public LoginUserWithGoogleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LoginUserResult> Handle(LoginUserWithGoogleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
