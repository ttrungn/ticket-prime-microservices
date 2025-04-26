using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
    {
        public void Configure(EntityTypeBuilder<TicketType> builder)
        {
            // Map to table
            builder.ToTable("TicketType");

            // Primary key
            builder.HasKey(tt => tt.Id);

            // Foreign key to Organizer
            builder.Property(tt => tt.OrganizerId)
                   .IsRequired();
            builder.HasOne(tt => tt.Organizer)
                   .WithMany(o => o.TicketTypes)
                   .HasForeignKey(tt => tt.OrganizerId);

            // Name and Description
            builder.Property(tt => tt.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(tt => tt.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            // Relationship to Tickets
            builder.HasMany(tt => tt.Tickets)
                   .WithOne(t => t.TicketType)
                   .HasForeignKey(t => t.TypeId);

            // Auditable fields
            builder.Property(tt => tt.CreatedAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAdd();

            builder.Property(tt => tt.LastModifiedAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAddOrUpdate();

            builder.Property(tt => tt.DeleteAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAdd();

            builder.Property(tt => tt.DeleteFlag)
                   .HasDefaultValue(false);
        }
    }
}
