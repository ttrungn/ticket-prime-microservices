namespace CoreService.Domain.Entities
{
    public class Organizer : BaseAuditableEntity<Guid>
    {
        public string UserId { get; set; } = default!;
        public string OrganizerCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public EmailAddress ContactEmail { get; set; } = default!;
        public PhoneNumber PhoneNumber { get; set; } = default!;
        public Uri AvatarUrl { get; set; } = default!;
        public Uri Website { get; set; } = default!;
        public string Bio { get; set; } = default!;
        public List<Event> Events { get; set; } = new();
    }
}
