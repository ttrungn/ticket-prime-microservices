using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // Table mapping
            builder.ToTable("Review");

            // Primary key
            builder.HasKey(r => r.Id);

            // Self-referencing relationship for replies
            builder.Property(r => r.ParentId)
                   .IsRequired(false);
            builder.HasOne<Review>()
                   .WithMany(r => r.Replies)
                   .HasForeignKey(r => r.ParentId);

            // CustomerId column
            builder.Property(r => r.CustomerId)
                   .IsRequired();
            // Customer Relationship
            builder.HasOne(r => r.Customer)
                   .WithMany(c => c.Reviews)
                   .HasForeignKey(r => r.CustomerId);

            // Event relationship
            builder.Property(r => r.EventId)
                   .IsRequired();
            builder.HasOne(r => r.Event)
                   .WithMany(e => e.Reviews)
                   .HasForeignKey(r => r.EventId);

            // Rating and Comment
            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .IsRequired()
                   .HasMaxLength(1000);

            // Auditable fields
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
