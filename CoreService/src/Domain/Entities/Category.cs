namespace CoreService.Domain.Entities
{
    public class Category : BaseAuditableEntity<Guid>
    {
        public string Name { get; set; } = default!;
        public string NormalizedName { get; set; } = default!;
        private readonly List<SubCategory> _subCategories = [];
        public IReadOnlyCollection<SubCategory> SubCategories => _subCategories.AsReadOnly();
        protected Category() { }
        public Category(string name, string normalizedName)
        {
            Name = NotEmpty(name, nameof(name));
            NormalizedName = NotEmpty(normalizedName, nameof(normalizedName));
        }
    }
}
