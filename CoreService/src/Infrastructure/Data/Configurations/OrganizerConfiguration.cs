using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            // Map to table
            builder.ToTable("Organizer");

            // Primary key
            builder.HasKey(o => o.Id);

            // UserId
            builder.Property(o => o.UserId)
                   .IsRequired()
                   .HasMaxLength(450);

            // OrganizerCode
            builder.Property(o => o.OrganizerCode)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasIndex(o => o.OrganizerCode)
                   .IsUnique();

            // Name
            builder.Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // ContactEmail (owned)
            builder.OwnsOne(o => o.ContactEmail, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("ContactEmail")
                     .IsRequired()
                     .HasMaxLength(254);
            });

            // PhoneNumber (owned)
            builder.OwnsOne(o => o.PhoneNumber, phone =>
            {
                phone.Property(p => p.Value)
                     .HasColumnName("PhoneNumber")
                     .IsRequired()
                     .HasMaxLength(15);
            });

            // AvatarUrl conversion
            builder.Property(o => o.AvatarUrl)
                   .HasConversion(
                       uri => uri.ToString(),
                       str => new Uri(str))
                   .HasColumnName("AvatarUrl")
                   .IsRequired(false)
                   .HasMaxLength(255);

            // Website conversion
            builder.Property(o => o.Website)
                   .HasConversion(
                       uri => uri.ToString(),
                       str => new Uri(str))
                   .HasColumnName("Website")
                   .IsRequired(false)
                   .HasMaxLength(255);

            // Bio
            builder.Property(o => o.Bio)
                   .IsRequired()
                   .HasMaxLength(1000);

            // Event Relationship
            builder.HasMany(o => o.Events)
                   .WithOne(e => e.Organizer)
                   .HasForeignKey(e => e.OrganizerId);

            // TicketType Relationship
            builder.HasMany(o => o.TicketTypes)
                   .WithOne(tt => tt.Organizer)
                   .HasForeignKey(tt => tt.OrganizerId);

            // TypeService Relationship
            builder.HasMany(o => o.TypeServices)
                   .WithOne(t => t.Organizer)
                   .HasForeignKey(t => t.OrganizerId);

            // Auditable fields
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
