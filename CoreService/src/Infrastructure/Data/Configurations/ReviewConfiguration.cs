using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Review");

            // Primary Key
            builder.HasKey(r => r.Id);

            // Properties configuration
            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(1000); // Adjust the max length of Comment as needed

            // Relationships configuration
            builder.HasOne(r => r.ParentReview)
                .WithMany(r => r.Replies)
                .HasForeignKey(r => r.ParentReviewId);

            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Reviews) // Assuming Customer has a navigation property back to Reviews
                .HasForeignKey(r => r.CustomerId);

            builder.HasOne(r => r.Event)
                .WithMany(e => e.Reviews) // Assuming Event has a navigation property back to Reviews
                .HasForeignKey(r => r.EventId);

            // Auditable fields configuration
            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(r => r.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
