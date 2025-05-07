namespace CoreService.Domain.Entities
{
    public class Review : BaseAuditableEntity<Guid>
    {
        public Guid? ParentReviewId { get; set; }
        public Review? ParentReview { get; set; }  
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;
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
        public string Comment { get; set; } = null!;
        public List<Review> Replies { get; set; } = new();
    }
}

