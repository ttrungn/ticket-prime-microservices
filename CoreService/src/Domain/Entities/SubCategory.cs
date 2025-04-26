namespace CoreService.Domain.Entities
{
    public class SubCategory : BaseAuditableEntity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string NormalizedName { get; private set; } = default!;
        protected SubCategory() { }
        public SubCategory(string name, string normalizedName)
        {
            Name = NotEmpty(name, nameof(name));
            NormalizedName = NotEmpty(normalizedName, nameof(normalizedName));
        }
    }
}
