using NotificationService.Domain.SharedEvents;

namespace NotificationService.Web.Consumers
{
    public class UserRegisteredEventConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly ILogger<UserRegisteredEventConsumer> _logger;
        public UserRegisteredEventConsumer(ILogger<UserRegisteredEventConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation("UserRegisteredEventConsumer: {UserId} {Email}", message.UserId, message.Email);
            return Task.CompletedTask;
        }
    }
}
