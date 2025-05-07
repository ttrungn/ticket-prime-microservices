using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Venue");

            // Primary Key
            builder.HasKey(v => v.Id);

            // Properties configuration
            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.Property(v => v.BackgroundImageUrl)
                .HasMaxLength(1000); // Adjust max length as needed

            builder.Property(v => v.Width)
                .IsRequired();

            builder.Property(v => v.Height)
                .IsRequired();

            // Relationships configuration with OnDelete behavior
            builder.HasOne(v => v.Event)
                .WithOne(e => e.Venue) // Assuming Event has a navigation property back to Venue
                .HasForeignKey<Venue>(v => v.EventId);

            builder.HasMany(v => v.TicketTypes)
                .WithOne(tt => tt.Venue) // Assuming TicketType has a navigation property back to Venue
                .HasForeignKey(tt => tt.VenueId);

            builder.HasMany(v => v.SeatSections)
                .WithOne(ss => ss.Venue) // Assuming SeatSection has a navigation property back to Venue
                .HasForeignKey(ss => ss.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Auditable fields configuration
            builder.Property(v => v.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(v => v.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(v => v.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(v => v.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
