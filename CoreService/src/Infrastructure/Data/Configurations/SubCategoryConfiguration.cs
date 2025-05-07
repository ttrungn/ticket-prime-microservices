using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("SubCategory");

            // Primary Key
            builder.HasKey(sc => sc.Id);

            // Properties configuration
            builder.Property(sc => sc.Name)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.Property(sc => sc.NormalizedName)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            // Relationships configuration with OnDelete behavior
            builder.HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories) // Assuming Category has a navigation property back to SubCategory
                .HasForeignKey(sc => sc.CategoryId);

            // Auditable fields configuration
            builder.Property(sc => sc.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(sc => sc.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(sc => sc.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(sc => sc.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
