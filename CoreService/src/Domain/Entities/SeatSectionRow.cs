namespace CoreService.Domain.Entities
{
    public class SeatSectionRow : BaseAuditableEntity<Guid>
    {
        public string RowNumber { get; set; } = null!;

        public double PositionX { get; set; }

        public double PositionY { get; set; }

        public string RowNumberPosition { get; set; } = null!;

        public Guid SeatSectionId { get; set; }

        public SeatSection SeatSection { get; set; } = null!;

        public List<Seat> Seats { get; set; } = new();
    }
}
