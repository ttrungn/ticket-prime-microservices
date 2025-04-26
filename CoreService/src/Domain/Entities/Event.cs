namespace CoreService.Domain.Entities
{
    public class Event : BaseAuditableEntity<Guid>
    {
        public Guid OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = default!;
        public Guid VenueId { get; set; }
        public Venue Venue { get; set; } = default!;
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; } = default!;
        public Uri ImageUrl { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;

        private int _totalTickets;
        public int TotalTickets
        {
            get => _totalTickets;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(TotalTickets), value, "Total tickets must be greater than 0.");
                _totalTickets = value;
            }
        }

        private int _totalTicketsAvailable;
        public int TotalTicketsAvailable
        {
            get => _totalTicketsAvailable;
            set
            {
                if (value <= 0 || value > TotalTickets)
                    throw new ArgumentOutOfRangeException(nameof(TotalTicketsAvailable), value, "Total tickets available must be greater than 0 and no more than the total tickets.");
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
                    throw new InvalidOperationException("Start time must be less than end time.");
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
                    throw new InvalidOperationException("End time must be greater than start time.");
                _endTime = value;
            }
        }

        private readonly List<Ticket> _tickets = [];
        public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();

        private readonly List<Review> _reviews = [];
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        protected Event() { }

        public Event(
            Guid id,
            Guid organizerId,
            Guid venueId,
            Guid subCategoryId,
            string code,
            string title,
            string description,
            int totalTickets,
            int totalTicketsAvailable,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            Uri imageUrl
        )
        {
            Id = id;
            OrganizerId = organizerId;
            VenueId = venueId;
            SubCategoryId = subCategoryId;
            Code = code;
            Title = NotEmpty(title, nameof(title));
            Description = description?.Trim() ?? string.Empty;
            TotalTickets = totalTickets;
            TotalTicketsAvailable = totalTicketsAvailable;
            if (endTime <= startTime)
                throw new Exception("EndTime must be after StartTime");
            StartTime = startTime;
            EndTime = endTime;
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
        }

        public void AddTicket(Ticket ticket)
        {
            if (ticket is null)
                throw new ArgumentNullException(nameof(ticket));

            if (_tickets.Any(t => t.Id == ticket.Id))
                throw new InvalidOperationException(
                    $"A ticket with Id {ticket.Id} has already been added.");

            _tickets.Add(ticket);
        }

        public void RemoveTicket(Ticket ticket)
        {
            if (ticket is null)
                throw new ArgumentNullException(nameof(ticket));

            if (!_tickets.Remove(ticket))
                throw new InvalidOperationException(
                    $"Ticket with Id {ticket.Id} is not associated with this event.");
        }

        public void AddReview(Review review)
        {
            if (review is null)
                throw new ArgumentNullException(nameof(review));

            if (_reviews.Any(r => r.Id == review.Id))
                throw new InvalidOperationException(
                    $"A review with Id {review.Id} has already been added.");

            _reviews.Add(review);
        }

        public void RemoveReview(Review review)
        {
            if (review is null)
                throw new ArgumentNullException(nameof(review));

            if (!_reviews.Remove(review))
                throw new InvalidOperationException(
                    $"Review with Id {review.Id} is not associated with this event.");
        }
    }
}

