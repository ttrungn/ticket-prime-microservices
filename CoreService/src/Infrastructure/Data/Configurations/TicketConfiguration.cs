using CoreService.Domain.Constants;
using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            // Table mapping
            builder.ToTable("Ticket");

            // Primary key
            builder.HasKey(t => t.Id);

            // Optional relationship to Customer
            builder.Property(t => t.CustomerId)
                   .IsRequired(false);
            builder.HasOne(t => t.Customer)
                   .WithMany(c => c.Tickets)
                   .HasForeignKey(t => t.CustomerId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Required relationship to Event
            builder.Property(t => t.EventId)
                   .IsRequired();
            builder.HasOne(t => t.Event)
                   .WithMany(e => e.Tickets)
                   .HasForeignKey(t => t.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Required relationship to Type
            builder.Property(t => t.TypeId)
                   .IsRequired();
            builder.HasOne(t => t.TicketType)
                   .WithMany(ty => ty.Tickets)
                   .HasForeignKey(t => t.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Required relationship to Seat
            builder.Property(t => t.SeatId)
                   .IsRequired();
            builder.HasOne(t => t.Seat)
                   .WithMany()
                   .HasForeignKey(t => t.SeatId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Price
            builder.Property(t => t.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // TicketStatus enum as int
            builder.Property(t => t.TicketStatus)
                   .HasConversion<int>()
                   .IsRequired();

            // ReservedUntil and SoldAt
            builder.Property(t => t.ReservedUntil)
                   .IsRequired(false);
            builder.Property(t => t.SoldAt)
                   .IsRequired(false);

            // IsUsed flag
            builder.Property(t => t.IsUsed)
                   .IsRequired();

            // Notes
            builder.Property(t => t.Notes)
                   .IsRequired(false)
                   .HasMaxLength(500);

            // Auditable fields
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
