namespace CoreService.Domain.Entities
{
    public class Venue : BaseAuditableEntity<Guid>
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        private int _capacity;
        public int Capacity
        {
            get => _capacity;
            set => _capacity = value < 1 ? throw new ArgumentOutOfRangeException(nameof(value), value, "Capacity must be at least 1.") : value;
        }
        public Address Address { get; set; } = default!;
        public decimal Longtitude { get; set; }
        public decimal Latitude { get; set; }
        public Uri ImageUrl { get; set; } = default!;
        private readonly List<SeatSection> _seatSections = [];
        public IReadOnlyCollection<SeatSection> SeatSections => _seatSections.AsReadOnly();
        private readonly List<Event> _events = [];
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();
        public Venue() { }

        public Venue(string code, string name, string description, int capacity, Address address, decimal longtitude, decimal latitude, Uri imageUrl)
        {
            Code = code;
            Name = name;
            Description = description;
            Capacity = capacity;
            Address = address;
            Longtitude = longtitude;
            Latitude = latitude;
            ImageUrl = imageUrl;
        }

        public void AddSeatSection(SeatSection section)
        {
            ArgumentNullException.ThrowIfNull(section);

            if (_seatSections.Any(s => s.Id == section.Id))
                throw new InvalidOperationException(
                    $"A seat section with Id {section.Id} has already been added.");

            _seatSections.Add(section);
        }

        public void RemoveSeatSection(Guid seatSectionId)
        {
            var existing = _seatSections.FirstOrDefault(s => s.Id == seatSectionId) ?? throw new InvalidOperationException(
                    $"No seat section with Id {seatSectionId} is associated with this venue.");
            _seatSections.Remove(existing);
        }

        public void AddEvent(Event eventItem)
        {
            ArgumentNullException.ThrowIfNull(eventItem);

            if (_events.Any(e => e.Id == eventItem.Id))
                throw new InvalidOperationException(
                    $"An event with Id {eventItem.Id} has already been added.");

            _events.Add(eventItem);
        }

        public void RemoveEvent(Guid eventId)
        {
            var existing = _events.FirstOrDefault(e => e.Id == eventId) ?? throw new InvalidOperationException(
                    $"No event with Id {eventId} is associated with this venue.");
            _events.Remove(existing);
        }

    }
}

