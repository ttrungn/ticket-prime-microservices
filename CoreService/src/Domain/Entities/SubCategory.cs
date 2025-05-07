namespace CoreService.Domain.Entities
{
    public class SubCategory : BaseAuditableEntity<Guid>
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string Name { get; private set; } = null!;
        public string NormalizedName { get; private set; } = null!;
    }
}
