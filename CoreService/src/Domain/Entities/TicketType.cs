namespace CoreService.Domain.Entities
{
    public class TicketType : BaseAuditableEntity<Guid>
    {
        public Guid OrganizerId { get; private set; }
        public Organizer Organizer { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        private readonly List<Ticket> _tickets = [];
        public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();
        private readonly List<TypeService> _typeServices = [];
        public IReadOnlyCollection<TypeService> TypeServices => _typeServices.AsReadOnly();
        public TicketType() { }
        public TicketType(Guid id, Guid organizerId, string name, string description)
        {
            Id = id;
            OrganizerId = organizerId;
            Name = NotEmpty(name, nameof(name));
            Description = description?.Trim() ?? string.Empty;
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

        public void AddTypeService(TypeService typeService)
        {
            if (typeService is null)
                throw new ArgumentNullException(nameof(typeService));

            if (_typeServices.Any(ts => ts.Id == typeService.Id))
                throw new InvalidOperationException(
                    $"A type service with Id {typeService.Id} has already been added.");

            _typeServices.Add(typeService);
        }

        public void RemoveTypeService(TypeService typeService)
        {
            if (typeService is null)
                throw new ArgumentNullException(nameof(typeService));

            if (!_typeServices.Remove(typeService))
                throw new InvalidOperationException(
                    $"Type service with Id {typeService.Id} is not associated with this ticket type.");
        }
    }
}
