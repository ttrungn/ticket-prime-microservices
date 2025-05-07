using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Organizer");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties configuration
            builder.Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.Property(o => o.OrganizerCode)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.OwnsOne(o => o.ContactEmail, email =>
            {
                email.Property(e => e.Value).IsRequired().HasMaxLength(255); // Adjust max length for email
            });

            builder.OwnsOne(o => o.PhoneNumber, phone =>
            {
                phone.Property(p => p.Value).IsRequired().HasMaxLength(20); // Adjust max length for phone number
            });

            builder.Property(o => o.AvatarUrl)
                .IsRequired()
                .HasMaxLength(1000); // Adjust max length for URL

            builder.Property(o => o.Website)
                .IsRequired()
                .HasMaxLength(1000); // Adjust max length for URL

            builder.Property(o => o.Bio)
                .IsRequired()
                .HasMaxLength(1000); // Adjust max length for Bio text

            // Relationships configuration with OnDelete behavior
            builder.HasMany(o => o.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerId);

            // Auditable fields configuration
            builder.Property(o => o.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(o => o.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
