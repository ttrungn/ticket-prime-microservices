using CoreService.Application.Common.Interfaces;
using CoreService.Application.Common.Security;
using Microsoft.Extensions.Logging;

namespace CoreService.Application.Test.Queries.GetTest;

[Authorize(Roles = "Customer")]
public record GetTestQuery : IRequest<string>
{
}

public class GetTestQueryValidator : AbstractValidator<GetTestQuery>
{
    public GetTestQueryValidator()
    {
    }
}

public class GetTestQueryHandler : IRequestHandler<GetTestQuery, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly ILogger<GetTestQueryHandler> _logger;

    public GetTestQueryHandler(IApplicationDbContext context, IUser user, ILogger<GetTestQueryHandler> logger)
    {
        _context = context;
        _user = user;
        _logger = logger;
    }

    public Task<string> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetTestQueryHandler called by user {UserId} ({UserName}, {Email}) with roles {Roles}", _user.Id, _user.UserName, _user.Email, string.Join(", ", _user.Roles));
        return Task.FromResult("Ok");
    }
}
