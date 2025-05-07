using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
    {
        public void Configure(EntityTypeBuilder<TicketType> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("TicketType");

            // Primary Key
            builder.HasKey(tt => tt.Id);

            // Properties configuration
            builder.Property(tt => tt.Name)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.Property(tt => tt.Color)
                .IsRequired()
                .HasMaxLength(7); // Assuming Color is in a hex format like "#FFFFFF"

            // Relationships configuration with OnDelete behavior
            builder.HasOne(tt => tt.Venue)
                .WithMany(v => v.TicketTypes) // Assuming Venue has a navigation property back to TicketType
                .HasForeignKey(tt => tt.VenueId);

            builder.HasMany(tt => tt.Tickets)
                .WithOne(t => t.TicketType) // Assuming Ticket has a navigation property back to TicketType
                .HasForeignKey(t => t.TicketTypeId);

            builder.HasMany(tt => tt.TypeServices)
                .WithOne(ts => ts.TicketType) // Assuming TypeService has a navigation property back to TicketType
                .HasForeignKey(ts => ts.TicketTypeId);

            builder.HasMany(tt => tt.Seats)
                .WithOne(s => s.TicketType) // Assuming Seat has a navigation property back to TicketType
                .HasForeignKey(s => s.TicketTypeId);

            // Auditable fields configuration
            builder.Property(tt => tt.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(tt => tt.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(tt => tt.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(tt => tt.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
