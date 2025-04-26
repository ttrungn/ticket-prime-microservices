namespace CoreService.Domain.Entities
{
    public class Review : BaseAuditableEntity<Guid>
    {
        public Guid? ParentId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public Guid EventId { get; set; }
        public Event Event { get; set; } = default!;
        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 1 and 5.");
                }
                _rating = value;
            }
        }
        public string Comment { get; set; } = default!;
        private readonly List<Review> _replies = new();
        public IReadOnlyCollection<Review> Replies => _replies.AsReadOnly();

        public Review()
        {
        }

        public Review(
            Guid customerId,
            Guid eventId,
            int rating,
            string comment,
            Guid? parentId = null)
        {
            Id = Guid.NewGuid();
            ParentId = parentId;
            CustomerId = customerId;
            EventId = eventId;
            Rating = rating;
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
        }

        public void AddReply(Review reply)
        {
            ArgumentNullException.ThrowIfNull(reply);
            if (_replies.Any(r => r.Id == reply.Id))
                throw new InvalidOperationException($"Reply {reply.Id} already added.");
            _replies.Add(reply);
        }

        public void RemoveReply(Guid replyId)
        {
            var existing = _replies.FirstOrDefault(r => r.Id == replyId) ?? throw new InvalidOperationException($"No reply with Id {replyId}.");
            _replies.Remove(existing);
        }
    }
}

