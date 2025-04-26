namespace CoreService.Domain.Entities
{
    public class Customer : BaseAuditableEntity<Guid>
    {
        public string UserId { get; set; } = default!;

        public string CustomerCode { get; set; } = default!;

        public Uri? AvatarUrl { get; set; } = default!;

        public Name Name { get; set; } = default!;

        public PhoneNumber PhoneNumber { get; set; } = default!;

        public Gender Gender { get; set; } = default!;

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value < 14)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Age must be at least 14.");
                }
                _age = value;
            }
        }
        private readonly List<Review> _reviews = [];
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
        private readonly List<Ticket> _tickets = [];
        public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();
        public Address Address { get; set; } = default!;

        protected Customer() { }

        public Customer(
            Guid id,
            string userId,
            string customerCode,
            Name name,
            PhoneNumber phoneNumber,
            Gender gender,
            int age,
            Address address,
            Uri? avatarUrl = null)
        {
            Id = id;
            UserId = NotEmpty(userId, nameof(userId));
            CustomerCode = NotEmpty(customerCode, nameof(customerCode));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Gender = gender;
            Age = age;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            AvatarUrl = avatarUrl;
        }

        public void UpdateProfile(
            Name name,
            PhoneNumber phoneNumber,
            Gender gender,
            int age,
            Address address,
            Uri? avatarUrl = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Gender = gender;
            Age = age;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            AvatarUrl = avatarUrl;
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

        public void AddTicket(Ticket ticket)
        {
            if (ticket is null)
                throw new ArgumentNullException(nameof(ticket));

            if (_tickets.Any(r => r.Id == ticket.Id))
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
    }
}

