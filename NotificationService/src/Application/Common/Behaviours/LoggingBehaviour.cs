using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Common.Interfaces;

namespace NotificationService.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IUser _user;

    public LoggingBehaviour(ILogger<TRequest> logger, IUser user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? "Anonymous";

        _logger.LogInformation("NotificationService Request: {Name} {@UserId} {@Request}",
            requestName, userId, request);

        await Task.CompletedTask;
    }
}
