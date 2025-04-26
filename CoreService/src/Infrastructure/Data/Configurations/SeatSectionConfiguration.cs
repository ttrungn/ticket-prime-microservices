using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class SeatSectionConfiguration : IEntityTypeConfiguration<SeatSection>
    {
        public void Configure(EntityTypeBuilder<SeatSection> builder)
        {
            // Map to table
            builder.ToTable("SeatSection");

            // Primary key
            builder.HasKey(s => s.Id);

            // Foreign key to Venue
            builder.Property(s => s.VenueId)
                   .IsRequired();
            builder.HasOne(s => s.Venue)
                   .WithMany(v => v.SeatSections)
                   .HasForeignKey(s => s.VenueId);

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

            // Relationship to rows
            builder.HasMany(s => s.SeatSectionRows)
                   .WithOne(r => r.SeatSection)
                   .HasForeignKey(r => r.SeatSectionId);

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
