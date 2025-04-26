using CoreService.Domain.Constants;

namespace CoreService.Domain.Entities
{
    public class Ticket : BaseAuditableEntity<Guid>
    {
        public Guid? CustomerId { get; set; }
        private Customer? _customer;
        public Customer? Customer
        {
            get => _customer;
            set
            {
                if (_customer != null)
                {
                    throw new InvalidOperationException("Customer is already assigned.");
                }
                _customer = value;
            }
        }
        public Guid EventId { get; set; }
        public Event Event { get; set; } = default!;
        public Guid TypeId { get; set; }
        public TicketType TicketType { get; set; } = default!;
        public Guid SeatId { get; set; }
        public Seat Seat { get; set; } = default!;
        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Price must be greater than or equal to 0");
                }

                _price = value;
            }
        }
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Available;
        public DateTimeOffset? ReservedUntil { get; private set; }
        public DateTimeOffset? SoldAt { get; private set; }
        public bool IsUsed { get; set; }
        public string Notes { get; set; } = default!;

        public Ticket()
        {
        }

        public Ticket(
            Guid id,
            Guid? customerId,
            Guid eventId,
            Guid typeId,
            Guid seatId,
            decimal price,
            bool isUsed,
            string notes)
        {
            Id = id;
            CustomerId = customerId;
            EventId = eventId;
            TypeId = typeId;
            SeatId = seatId;
            Price = price;
            IsUsed = isUsed;
            Notes = notes;
        }

        public void Reserve()
        {
            if (TicketStatus != TicketStatus.Available)
            {
                throw new InvalidOperationException("Ticket must be available to reserve.");
            }

            if (Customer == null)
            {
                throw new InvalidOperationException("A customer must be assigned to reserve the ticket.");
            }

            TicketStatus = TicketStatus.Reserved;
            ReservedUntil = DateTimeOffset.UtcNow.AddMinutes(TicketConstants.ReservedUntilTimeInMinutes);
        }

        public void Sold()
        {
            if (TicketStatus != TicketStatus.Reserved)
            {
                throw new InvalidOperationException("Ticket must be reserved before it can be sold.");
            }

            if (ReservedUntil != DateTimeOffset.UtcNow)
            {
                throw new InvalidOperationException("The reservation has expired. The ticket cannot be sold.");
            }

            if (Customer == null)
            {
                throw new InvalidOperationException("A customer must be assigned before the ticket can be sold.");
            }

            TicketStatus = TicketStatus.Sold;
            ReservedUntil = null;
            SoldAt = DateTimeOffset.UtcNow;
        }
    }
}

