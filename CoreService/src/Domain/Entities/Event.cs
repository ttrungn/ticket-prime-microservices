namespace CoreService.Domain.Entities
{
    public class Event : BaseAuditableEntity<Guid>
    {
        public Guid OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;
        public Guid VenueId { get; set; }
        public Venue Venue { get; set; } = null!;
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; } = null!;
        public Uri ImageUrl { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Address Address { get; set; } = null!;

        private int _totalTickets;
        public int TotalTickets
        {
            get => _totalTickets;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(TotalTickets), value, "Total tickets must be greater than 0.");
                }

                _totalTickets = value;
            }
        }

        private int _totalTicketsAvailable;
        public int TotalTicketsAvailable
        {
            get => _totalTicketsAvailable;
            set
            {
                if (value <= 0 || value > _totalTickets)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(TotalTicketsAvailable), value, "Total tickets available must be greater than 0 and no more than the total tickets.");
                }

                _totalTicketsAvailable = value;
            }
        }

        private DateTimeOffset _startTime;
        public DateTimeOffset StartTime
        {
            get => _startTime;
            set
            {
                if (_endTime != default && value > _endTime)
                {
                    throw new InvalidOperationException("Start time must be less than end time.");
                }

                _startTime = value;
            }
        }

        private DateTimeOffset _endTime;
        public DateTimeOffset EndTime
        {
            get => _endTime;
            set
            {
                if (_startTime != default && value < _startTime)
                {
                    throw new InvalidOperationException("End time must be greater than start time.");
                }

                _endTime = value;
            }
        }

        public List<Ticket> Tickets { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
    }
}
