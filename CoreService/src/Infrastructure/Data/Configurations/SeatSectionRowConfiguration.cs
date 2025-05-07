using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class SeatSectionRowConfiguration : IEntityTypeConfiguration<SeatSectionRow>
    {
        public void Configure(EntityTypeBuilder<SeatSectionRow> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("SeatSectionRow");

            // Primary Key
            builder.HasKey(ssr => ssr.Id);

            // Properties configuration
            builder.Property(ssr => ssr.RowNumber)
                .IsRequired()
                .HasMaxLength(50); // Adjust the max length as needed

            builder.Property(ssr => ssr.PositionX)
                .IsRequired();

            builder.Property(ssr => ssr.PositionY)
                .IsRequired();

            builder.Property(ssr => ssr.RowNumberPosition)
                .IsRequired()
                .HasMaxLength(50); // Adjust the max length as needed

            // Relationships configuration with OnDelete behavior
            builder.HasOne(ssr => ssr.SeatSection)
                .WithMany(ss => ss.SeatSectionRows) // Assuming SeatSection has a navigation property back to SeatSectionRow
                .HasForeignKey(ssr => ssr.SeatSectionId);

            builder.HasMany(ssr => ssr.Seats)
                .WithOne(s => s.SeatSectionRow) // Assuming Seat has a navigation property back to SeatSectionRow
                .HasForeignKey(s => s.SeatSectionRowId)
                .OnDelete(DeleteBehavior.Cascade);

            // Auditable fields configuration
            builder.Property(ssr => ssr.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(ssr => ssr.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(ssr => ssr.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(ssr => ssr.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
