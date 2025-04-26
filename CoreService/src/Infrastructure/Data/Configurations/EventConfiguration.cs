using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // Map to table
            builder.ToTable("Event");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Foreign keys and navigations
            builder.Property(e => e.OrganizerId)
                   .IsRequired();
            builder.HasOne(e => e.Organizer)
                   .WithMany(o => o.Events)
                   .HasForeignKey(e => e.OrganizerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.VenueId)
                   .IsRequired();
            builder.HasOne(e => e.Venue)
                   .WithMany(v => v.Events)
                   .HasForeignKey(e => e.VenueId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.SubCategoryId)
                   .IsRequired();
            builder.HasOne(e => e.SubCategory)
                   .WithMany()
                   .HasForeignKey(e => e.SubCategoryId);

            // ImageUrl conversion
            builder.Property(e => e.ImageUrl)
                   .HasConversion(
                       uri => uri.ToString(),
                       str => new Uri(str))
                   .IsRequired()
                   .HasColumnName("ImageUrl");

            // Scalar properties
            builder.Property(e => e.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(e => e.TotalTickets)
                   .IsRequired();

            builder.Property(e => e.TotalTicketsAvailable)
                   .IsRequired();

            builder.Property(e => e.StartTime)
                   .IsRequired();

            builder.Property(e => e.EndTime)
                   .IsRequired();

            // One-to-many relationships
            builder.HasMany(e => e.Tickets)
                   .WithOne(t => t.Event)
                   .HasForeignKey(t => t.EventId);

            builder.HasMany(e => e.Reviews)
                   .WithOne(r => r.Event)
                   .HasForeignKey(r => r.EventId);

            // Auditable fields
            builder.Property(e => e.CreatedAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.LastModifiedAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAddOrUpdate();

            builder.Property(e => e.DeleteAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.DeleteFlag)
                   .HasDefaultValue(false);
        }
    }
}
