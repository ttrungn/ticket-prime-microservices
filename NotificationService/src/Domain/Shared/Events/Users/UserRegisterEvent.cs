namespace NotificationService.Domain.Shared.Events.Users;

public class UserRegisteredEvent
{
    public string UserId { get; }
    public string Email { get; }

    public UserRegisteredEvent(string userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}

