using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Table configuration (optional, will default to table name from DbSet)
            builder.ToTable("Customer");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties configuration
            builder.Property(c => c.UserId)
                .IsRequired()
                .HasMaxLength(255); // You can adjust the max length as needed

            builder.Property(c => c.CustomerCode)
                .IsRequired()
                .HasMaxLength(255); // You can adjust the max length as needed

            builder.Property(c => c.AvatarUrl)
                .HasMaxLength(1000); // Adjust URL length as needed

            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.First).IsRequired().HasMaxLength(255);
                name.Property(n => n.Last).IsRequired().HasMaxLength(255);
            });

            builder.OwnsOne(c => c.PhoneNumber, phone =>
            {
                phone.Property(p => p.Value).IsRequired().HasMaxLength(20); // Adjust max length as needed
            });

            // Relationships
            builder.HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);

            builder.HasMany(c => c.Tickets)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId);

            builder.OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.Street).IsRequired().HasMaxLength(255);
                address.Property(a => a.City).IsRequired().HasMaxLength(255);
                address.Property(a => a.Country).IsRequired().HasMaxLength(255);
            });

            builder.Property(c => c.Gender)
                .IsRequired();

            builder.Property(c => c.Age)
                .IsRequired();

            // Auditable fields configuration
            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.LastModifiedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(c => c.DeleteAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.DeleteFlag)
                .HasDefaultValue(false);
        }
    }
}
