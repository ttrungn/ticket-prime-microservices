using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Event");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties configuration
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50); // Adjust the length as needed

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255); // Adjust the length as needed

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000); // Adjust the length as needed

            builder.Property(e => e.ImageUrl)
                .IsRequired()
                .HasMaxLength(1000); // Adjust the length as needed

            builder.OwnsOne(e => e.Address, address =>
            {
                address.Property(a => a.Street).IsRequired().HasMaxLength(255);
                address.Property(a => a.City).IsRequired().HasMaxLength(255);
                address.Property(a => a.Country).IsRequired().HasMaxLength(255);
            });

            builder.Property(e => e.TotalTickets)
                .IsRequired();

            builder.Property(e => e.TotalTicketsAvailable)
                .IsRequired();

            builder.Property(e => e.StartTime)
                .IsRequired();

            builder.Property(e => e.EndTime)
                .IsRequired();

            // Relationships configuration
            builder.HasOne(e => e.Organizer)
                .WithMany(o => o.Events) // Assuming Organizer has a navigation property back to Event (optional)
                .HasForeignKey(e => e.OrganizerId);

            builder.HasOne(e => e.SubCategory)
                .WithMany() // Assuming SubCategory has a navigation property back to Event (optional)
                .HasForeignKey(e => e.SubCategoryId);

            builder.HasOne(e => e.Venue)
                .WithOne()
                .HasForeignKey<Event>(e => e.VenueId);
            
            builder.HasMany(e => e.Tickets)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventId);

            builder.HasMany(e => e.Reviews)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId);

            // Auditable fields configuration
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
