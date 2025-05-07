namespace CoreService.Domain.Entities
{
    public class TicketType : BaseAuditableEntity<Guid>
    {
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public Guid VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public List<Ticket> Tickets { get; set; } = new();
        public List<TypeService> TypeServices { get; set; } = new();
        public List<Seat> Seats { get; set; } = new();
    }
}
