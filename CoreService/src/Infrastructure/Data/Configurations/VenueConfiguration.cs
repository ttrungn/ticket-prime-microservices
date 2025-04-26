using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            // Map to table
            builder.ToTable("Venue");

            // Primary key
            builder.HasKey(v => v.Id);

            // Code, Name, Description
            builder.Property(v => v.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(v => v.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(v => v.Description)
                   .IsRequired()
                   .HasMaxLength(255);

            // Capacity
            builder.Property(v => v.Capacity)
                   .IsRequired();

            // Owned Address value-object
            builder.OwnsOne(v => v.Address, address =>
            {
                address.Property(a => a.Street)
                       .HasColumnName("Street")
                       .IsRequired()
                       .HasMaxLength(200);

                address.Property(a => a.City)
                       .HasColumnName("City")
                       .IsRequired()
                       .HasMaxLength(100);

                address.Property(a => a.Country)
                       .HasColumnName("Country")
                       .IsRequired()
                       .HasMaxLength(100);

                address.Property(a => a.ZipCode)
                       .HasColumnName("ZipCode")
                       .IsRequired()
                       .HasMaxLength(20);
            });

            // Coordinates
            builder.Property(v => v.Longtitude)
                   .IsRequired();

            builder.Property(v => v.Latitude)
                   .IsRequired();

            // ImageUrl conversion
            builder.Property(v => v.ImageUrl)
                   .IsRequired()
                   .HasConversion(
                       uri => uri.ToString(),
                       str => new Uri(str))
                   .HasColumnName("ImageUrl");

            // Relationship to SeatSections
            builder.HasMany(v => v.SeatSections)
                   .WithOne(s => s.Venue)
                   .HasForeignKey(s => s.VenueId);

            // Relationship to Events
            builder.HasMany(v => v.Events)
                   .WithOne(e => e.Venue)
                   .HasForeignKey(e => e.VenueId);

            // Auditable fields
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
