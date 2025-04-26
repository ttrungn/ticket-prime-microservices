namespace CoreService.Domain.Entities
{
    public class SeatSectionRow : BaseAuditableEntity<Guid>
    {
        public Guid SeatSectionId { get; set; }
        public SeatSection SeatSection { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        private readonly List<Seat> _seats = new();
        public IReadOnlyCollection<Seat> Seats => _seats.AsReadOnly();

        public SeatSectionRow() { }

        public SeatSectionRow(Guid id, Guid seatSectionId, string code, string name, string description)
        {
            Id = id;
            SeatSectionId = seatSectionId;
            Code = code;
            Name = NotEmpty(name, nameof(name));
            Description = NotEmpty(description, nameof(description));
        }

        public void AddSeat(Seat seat)
        {
            ArgumentNullException.ThrowIfNull(seat);

            if (_seats.Any(s => s.Id == seat.Id))
                throw new InvalidOperationException(
                    $"A seat with Id {seat.Id} has already been added.");

            _seats.Add(seat);
        }

        public void RemoveSeat(Guid seatId)
        {
            var existing = _seats.FirstOrDefault(s => s.Id == seatId) ?? throw new InvalidOperationException(
                $"No seat with Id {seatId} is associated with this seat section row.");
            _seats.Remove(existing);
        }
    }
}

