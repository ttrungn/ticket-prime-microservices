namespace CoreService.Domain.Entities
{
    public class Organizer : BaseAuditableEntity<Guid>
    {
        public string UserId { get; set; } = default!;
        public string OrganizerCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public EmailAddress ContactEmail { get; set; } = default!;
        public PhoneNumber PhoneNumber { get; set; } = default!;
        public Uri AvatarUrl { get; set; } = default!;
        public Uri Website { get; set; } = default!;
        public string Bio { get; set; } = default!;
        private readonly List<Event> _events = new();
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();
        private readonly List<TicketType> _ticketTypes = [];
        public IReadOnlyCollection<TicketType> TicketTypes => _ticketTypes.AsReadOnly();
        private readonly List<TypeService> _typeServices = [];
        public IReadOnlyCollection<TypeService> TypeServices => _typeServices.AsReadOnly();
        protected Organizer() { }

        public Organizer(
            Guid id,
            string userId,
            string organizerCode,
            string name,
            EmailAddress contactEmail,
            PhoneNumber phoneNumber,
            string bio,
            Uri avatarUrl,
            Uri website)
        {
            Id = id;
            UserId = NotEmpty(userId, nameof(userId));
            OrganizerCode = NotEmpty(organizerCode, nameof(organizerCode));
            Name = NotEmpty(name, nameof(name));
            ContactEmail = contactEmail;
            PhoneNumber = phoneNumber;
            Bio = bio?.Trim() ?? string.Empty;
            AvatarUrl = avatarUrl;
            Website = website;
        }

        public void UpdateProfile(
            string name,
            EmailAddress contactEmail,
            PhoneNumber phoneNumber,
            string bio,
            Uri avatarUrl,
            Uri website)
        {
            Name = NotEmpty(name, nameof(name));
            ContactEmail = contactEmail;
            PhoneNumber = phoneNumber;
            Bio = bio?.Trim() ?? Bio;
            AvatarUrl = avatarUrl;
            Website = website;
        }

        public void AddEvent(Event @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            if (_events.Any(e => e.Id == @event.Id))
            {
                throw new InvalidOperationException($"Event with Id {@event.Id} is already associated with this organizer.");
            }

            _events.Add(@event);
        }

        public void RemoveEvent(Event @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            if (!_events.Remove(@event))
            {
                throw new InvalidOperationException($"Event with Id {@event.Id} is not associated with this organizer.");
            }
        }

        public void AddTicketType(TicketType ticketType)
        {
            ArgumentNullException.ThrowIfNull(ticketType);

            if (_ticketTypes.Any(t => t.Id == ticketType.Id))
            {
                throw new InvalidOperationException($"Ticket type with Id {ticketType.Id} is already associated with this organizer.");
            }

            _ticketTypes.Add(ticketType);
        }

        public void RemoveTicketType(TicketType ticketType)
        {
            ArgumentNullException.ThrowIfNull(ticketType);

            if (!_ticketTypes.Remove(ticketType))
            {
                throw new InvalidOperationException($"Ticket type with Id {ticketType.Id} is not associated with this organizer.");
            }
        }

        public void AddTypeService(TypeService typeService)
        {
            ArgumentNullException.ThrowIfNull(typeService);

            if (_typeServices.Any(ts => ts.Id == typeService.Id))
            {
                throw new InvalidOperationException($"Type service with Id {typeService.Id} is already associated with this organizer.");
            }

            _typeServices.Add(typeService);
        }

        public void RemoveTypeService(TypeService typeService)
        {
            ArgumentNullException.ThrowIfNull(typeService);

            if (!_typeServices.Remove(typeService))
            {
                throw new InvalidOperationException($"Type service with Id {typeService.Id} is not associated with this organizer.");
            }
        }
    }
}


