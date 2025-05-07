namespace CoreService.Domain.Entities
{
    public class Category : BaseAuditableEntity<Guid>
    {
        public string Name { get; set; } = null!;
        public string NormalizedName { get; set; } = null!;
        public List<SubCategory> SubCategories { get; set; } = new();
    }
}
