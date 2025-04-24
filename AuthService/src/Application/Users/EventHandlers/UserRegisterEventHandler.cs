using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Events;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Users.EventHandlers
{
    public class UserRegisterEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        private readonly ILogger<UserRegisterEventHandler> _logger;
        private readonly IMassTransitService<UserRegisteredEvent> _massTransitService;
        public UserRegisterEventHandler(
            ILogger<UserRegisterEventHandler> logger,
            IMassTransitService<UserRegisteredEvent> massTransitService
        )
        {
            _logger = logger;
            _massTransitService = massTransitService;
        }
        public async Task Handle(UserRegisteredEvent userRegisteredEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received user registration event for UserId: {UserId} and Email: {Email}", userRegisteredEvent.UserId, userRegisteredEvent.Email);
            // await _massTransitService.Produce(userRegisteredEvent, cancellationToken);
            await Task.CompletedTask;
        }
    }
}

