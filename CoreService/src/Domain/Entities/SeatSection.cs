namespace CoreService.Domain.Entities
{
    public class SeatSection : BaseAuditableEntity<Guid>
    {
        public Guid VenueId { get; set; }
        public Venue Venue { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        private readonly List<SeatSectionRow> _seatSectionRows = [];

        public IReadOnlyCollection<SeatSectionRow> SeatSectionRows => _seatSectionRows.AsReadOnly();

        public SeatSection() { }

        public SeatSection(
            Guid id,
            Guid venueId,
            string code,
            string name,
            string description)
        {
            Id = id;
            VenueId = venueId;
            Code = code;
            Name = NotEmpty(name, nameof(name));
            Description = NotEmpty(description, nameof(description));
        }

        public void AddSeatSectionRow(SeatSectionRow seatSectionRow)
        {
            ArgumentNullException.ThrowIfNull(seatSectionRow);

            if (_seatSectionRows.Any(s => s.Id == seatSectionRow.Id))
            {
                throw new InvalidOperationException(
                    $"SeatSectionRow with Id {seatSectionRow.Id} is already associated with this seat section.");
            }

            _seatSectionRows.Add(seatSectionRow);
        }

        public void RemoveSeatSectionRow(SeatSectionRow seatSectionRow)
        {
            ArgumentNullException.ThrowIfNull(seatSectionRow);

            if (!_seatSectionRows.Remove(seatSectionRow))
            {
                throw new InvalidOperationException(
                    $"SeatSectionRow with Id {seatSectionRow.Id} is not associated with this seat section.");
            }
        }
    }
}

