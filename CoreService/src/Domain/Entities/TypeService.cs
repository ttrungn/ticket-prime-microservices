namespace CoreService.Domain.Entities
{
    public class TypeService : BaseAuditableEntity<Guid>
    {
        public Guid OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = default!;
        public Guid TypeId { get; set; }
        public TicketType TicketType { get; set; } = default!;
        public string Name { get; set; } = default!;

        public TypeService(Guid id, Guid organizerId, Guid typeId, string name)
        {
            Id = id;
            OrganizerId = organizerId;
            TypeId = typeId;
            Name = NotEmpty(name, nameof(name));
        }
    }
}
