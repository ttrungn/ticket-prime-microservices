using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Category");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties configuration
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255); // You can adjust the max length as needed

            builder.Property(c => c.NormalizedName)
                .IsRequired()
                .HasMaxLength(255); // You can adjust the max length as needed

            // Relationships configuration with OnDelete behavior
            builder.HasMany(c => c.SubCategories)
                .WithOne(sc =>
                    sc.Category) // Assuming the SubCategory has a navigation property back to Category, adjust if necessary
                .HasForeignKey(sc => sc.CategoryId); // You can customize the foreign key column name

            // Indexing
            builder.HasIndex(c => c.NormalizedName).IsUnique(); // Assuming you want to enforce uniqueness on the NormalizedName
            
            // Auditable fields (inherited)
            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(c => c.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
