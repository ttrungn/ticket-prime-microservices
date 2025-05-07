using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class TypeServiceConfiguration : IEntityTypeConfiguration<TypeService>
    {
        public void Configure(EntityTypeBuilder<TypeService> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("TypeService");

            // Primary Key
            builder.HasKey(ts => ts.Id);

            // Properties configuration
            builder.Property(ts => ts.Name)
                .IsRequired()
                .HasMaxLength(255); // Adjust the max length as needed

            builder.HasOne(ts => ts.TicketType)
                .WithMany(tt => tt.TypeServices) // Assuming TicketType has a navigation property back to TypeService
                .HasForeignKey(ts => ts.TicketTypeId);

            // Auditable fields configuration
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
