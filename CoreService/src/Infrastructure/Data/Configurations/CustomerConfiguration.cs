using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreService.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(c => c.Id);

            // UserId (e.g. ASP.NET Identity default length)
            builder.Property(c => c.UserId)
                   .IsRequired()
                   .HasMaxLength(450);

            // CustomerCode (adjust length to suit your needs)
            builder.Property(c => c.CustomerCode)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.HasIndex(c => c.CustomerCode)
                   .IsUnique();

            // AvatarUrl conversion from/to string
            builder.Property(c => c.AvatarUrl)
                   .HasConversion(
                       uri => uri != null ? uri.ToString() : null,
                       str => !string.IsNullOrEmpty(str) ? new Uri(str) : null)
                   .HasColumnName("AvatarUrl");

            // Gender as int
            builder.Property(c => c.Gender)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(c => c.Age)
                   .IsRequired();

            // Owned Name value‐object
            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.First)
                    .HasColumnName("FirstName")
                    .IsRequired()
                    .HasMaxLength(100);

                name.Property(n => n.Last)
                    .HasColumnName("LastName")
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Owned PhoneNumber value‐object
            builder.OwnsOne(c => c.PhoneNumber, phone =>
            {
                phone.Property(p => p.Value)
                     .HasColumnName("PhoneNumber")
                     .HasMaxLength(15);
            });

            // Owned Address value‐object
            builder.OwnsOne(c => c.Address, address =>
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

            // Review Relationship
            builder.HasMany(c => c.Reviews)
                   .WithOne(r => r.Customer)
                   .HasForeignKey(r => r.CustomerId);

            // Ticket Relationship
            builder.HasMany(c => c.Tickets)
                   .WithOne(t => t.Customer)
                   .HasForeignKey(t => t.CustomerId);

            // Auditable fields (inherited)
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
