namespace CoreService.Domain.Entities
{
    public class Venue : BaseAuditableEntity<Guid>
    {
        public Guid OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? BackgroundImageUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<TicketType> TicketTypes { get; set; } = new();
        public List<SeatSection> SeatSections { get; set; } = new();
    }
}
