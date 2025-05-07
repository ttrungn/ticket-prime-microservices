using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Persistence.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Seat");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties configuration
            builder.Property(s => s.SeatNumber)
                .IsRequired()
                .HasMaxLength(50); // Adjust the length as needed

            builder.Property(s => s.SeatGuid)
                .IsRequired();

            builder.Property(s => s.PositionX)
                .IsRequired();

            builder.Property(s => s.PositionY)
                .IsRequired();

            builder.Property(s => s.Radius)
                .IsRequired();

            builder.Property(s => s.SeatStatus)
                .IsRequired()
                .HasConversion<string>(); // Ensures the SeatStatus enum is stored as a string in the DB

            // Relationships configuration with OnDelete behavior
            builder.HasOne(s => s.SeatSectionRow)
                .WithMany(sr => sr.Seats) // Assuming SeatSectionRow doesn't have a navigation property back to Seat
                .HasForeignKey(s => s.SeatSectionRowId);

            builder.HasOne(s => s.TicketType)
                .WithMany(tt => tt.Seats) // Assuming TicketType doesn't have a navigation property back to Seat
                .HasForeignKey(s => s.TicketTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Auditable fields configuration
            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(s => s.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
