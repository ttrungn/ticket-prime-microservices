namespace CoreService.Domain.Entities
{
    public class Seat : BaseAuditableEntity<Guid>
    {
        public Guid SeatSectionRowId { get; set; }
        public SeatSectionRow SeatSectionRow { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public Seat() { }

        public Seat(Guid seatSectionRowId, string code, string name, string description)
        {
            SeatSectionRowId = seatSectionRowId;
            Code = code;
            Name = NotEmpty(name, nameof(name));
            Description = NotEmpty(description, nameof(description));
        }
    }
}

