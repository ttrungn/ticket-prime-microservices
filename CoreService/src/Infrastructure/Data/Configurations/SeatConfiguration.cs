using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            // Map to table
            builder.ToTable("Seat");

            // Primary key
            builder.HasKey(s => s.Id);

            // Foreign key property
            builder.Property(s => s.SeatSectionRowId)
                   .IsRequired();

            // Navigation: SeatSectionRow
            builder.HasOne(s => s.SeatSectionRow)
                   .WithMany(r => r.Seats)
                   .HasForeignKey(s => s.SeatSectionRowId);

            // Code, Name, Description
            builder.Property(s => s.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Description)
                   .IsRequired()
                   .HasMaxLength(255);

            // Auditable fields
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
