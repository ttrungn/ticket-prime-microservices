namespace CoreService.Domain.Entities
{
    public class SeatSection : BaseAuditableEntity<Guid>
    {
        public Guid VenueId { get; set; }
        public Venue Venue { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public List<SeatSectionRow> SeatSectionRows { get; set; } = new();
    }
}
