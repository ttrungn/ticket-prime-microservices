using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Ticket");

            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties configuration
            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); // Adjust precision as needed

            builder.Property(t => t.TicketStatus)
                .IsRequired()
                .HasConversion<int>(); // Enum will be stored as an integer in the DB

            builder.Property(t => t.Notes)
                .IsRequired()
                .HasMaxLength(1000); // Adjust max length as needed

            // Relationships configuration with OnDelete behavior
            builder.HasOne(t => t.Customer)
                .WithMany(c => c.Tickets) // Assuming Customer has a navigation property back to Ticket
                .HasForeignKey(t => t.CustomerId);

            builder.HasOne(t => t.Event)
                .WithMany(e => e.Tickets) // Assuming Event has a navigation property back to Ticket
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.TicketType)
                .WithMany(tt => tt.Tickets) // Assuming TicketType has a navigation property back to Ticket
                .HasForeignKey(t => t.TicketTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Seat)
                .WithOne() // Assuming Seat doesn't have a navigation property back to Ticket
                .HasForeignKey<Ticket>(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // Auditable fields configuration
            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(t => t.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
