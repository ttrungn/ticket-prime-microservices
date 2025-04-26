using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class SeatSectionRowConfiguration : IEntityTypeConfiguration<SeatSectionRow>
    {
        public void Configure(EntityTypeBuilder<SeatSectionRow> builder)
        {
            // Map to table
            builder.ToTable("SeatSectionRow");

            // Primary key
            builder.HasKey(r => r.Id);

            // Foreign key to SeatSection
            builder.Property(r => r.SeatSectionId)
                   .IsRequired();
            builder.HasOne(r => r.SeatSection)
                   .WithMany(s => s.SeatSectionRows)
                   .HasForeignKey(r => r.SeatSectionId);

            // Code, Name, Description
            builder.Property(r => r.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.Description)
                   .IsRequired()
                   .HasMaxLength(255);

            // Relationship to Seats (back-reference)
            builder.HasMany(r => r.Seats)
                   .WithOne(s => s.SeatSectionRow)
                   .HasForeignKey(s => s.SeatSectionRowId);

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
