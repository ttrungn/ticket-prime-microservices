namespace CoreService.Domain.Entities
{
    public class Customer : BaseAuditableEntity<Guid>
    {
        public string UserId { get; set; } = null!;

        public string CustomerCode { get; set; } = null!;

        public Uri? AvatarUrl { get; set; }

        public Name Name { get; set; } = null!;

        public PhoneNumber PhoneNumber { get; set; } = null!;

        public Gender Gender { get; set; }

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value < 14)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Age must be at least 14.");
                }
                _age = value;
            }
        }

        public List<Review> Reviews { get; set; } = new();

        public List<Ticket> Tickets { get; set; } = new();

        public Address Address { get; set; } = null!;
    }
}
