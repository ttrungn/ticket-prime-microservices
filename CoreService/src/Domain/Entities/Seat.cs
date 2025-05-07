namespace CoreService.Domain.Entities
{
    public class Seat : BaseAuditableEntity<Guid>
    {
        public string SeatNumber { get; set; } = null!;
        public Guid SeatGuid { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public float Radius { get; set; }

        public SeatStatus SeatStatus { get; set; } = SeatStatus.Available;

        public Guid SeatSectionRowId { get; set; }
        public SeatSectionRow SeatSectionRow { get; set; } = null!;

        public Guid TicketTypeId { get; set; }
        public TicketType TicketType { get; set; } = null!;
    }
}
