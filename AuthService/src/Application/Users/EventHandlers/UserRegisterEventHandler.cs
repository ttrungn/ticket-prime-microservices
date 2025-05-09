using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Events;
using AuthService.Domain.Events.Users;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Users.EventHandlers;

public class UserRegisterEventHandler(
    ILogger<UserRegisterEventHandler> logger,
    IMassTransitService<UserRegisteredEvent> massTransitService)
    : INotificationHandler<UserRegisteredEvent>
{
    private readonly IMassTransitService<UserRegisteredEvent> _massTransitService = massTransitService;

    public async Task Handle(UserRegisteredEvent userRegisteredEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received user registration event for UserId: {UserId} and Email: {Email}",
            userRegisteredEvent.UserId, userRegisteredEvent.Email);
        await _massTransitService.Produce(userRegisteredEvent, cancellationToken);
    }
}
