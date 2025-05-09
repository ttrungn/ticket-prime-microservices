namespace CoreService.Domain.Entities
{
    public class TypeService : BaseAuditableEntity<Guid>
    {
        public Guid TicketTypeId { get; set; }
        public TicketType TicketType { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
