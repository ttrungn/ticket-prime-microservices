namespace AuthService.Domain.Events
{
    public class UserRegisteredEvent : BaseEvent
    {
        public string UserId { get; }
        public string Email { get; }

        public UserRegisteredEvent(string userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
