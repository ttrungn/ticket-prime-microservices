using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class TypeServiceConfiguration : IEntityTypeConfiguration<TypeService>
    {
        public void Configure(EntityTypeBuilder<TypeService> builder)
        {
            // Map to table
            builder.ToTable("TypeService");

            // Primary key
            builder.HasKey(ts => ts.Id);

            // Foreign key to Organizer
            builder.Property(ts => ts.OrganizerId)
                   .IsRequired();
            builder.HasOne(ts => ts.Organizer)
                   .WithMany(o => o.TypeServices)
                   .HasForeignKey(ts => ts.OrganizerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Foreign key to TicketType
            builder.Property(ts => ts.TypeId)
                   .IsRequired();
            builder.HasOne(ts => ts.TicketType)
                   .WithMany(tt => tt.TypeServices)
                   .HasForeignKey(ts => ts.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Name property
            builder.Property(ts => ts.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Auditable fields
            builder.Property(ts => ts.CreatedAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAdd();

            builder.Property(ts => ts.LastModifiedAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAddOrUpdate();

            builder.Property(ts => ts.DeleteAt)
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                   .ValueGeneratedOnAdd();

            builder.Property(ts => ts.DeleteFlag)
                   .HasDefaultValue(false);
        }
    }
}
