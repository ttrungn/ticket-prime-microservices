using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class SeatSectionConfiguration : IEntityTypeConfiguration<SeatSection>
    {
        public void Configure(EntityTypeBuilder<SeatSection> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("SeatSection");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties configuration
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.Property(s => s.PositionX)
                .IsRequired();

            builder.Property(s => s.PositionY)
                .IsRequired();

            // Relationships configuration with OnDelete behavior
            builder.HasOne(s => s.Venue)
                .WithMany(v => v.SeatSections) // Assuming Venue has a navigation property back to SeatSections
                .HasForeignKey(s => s.VenueId);

            builder.HasMany(s => s.SeatSectionRows)
                .WithOne(sr => sr.SeatSection) // Assuming SeatSectionRow has a navigation property back to SeatSection
                .HasForeignKey(sr => sr.SeatSectionId)
                .OnDelete(DeleteBehavior.Cascade);

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
