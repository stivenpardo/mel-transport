using MelTransport.Domain.Entities;
using MelTransport.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelTransport.Infrastructure.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderNumber)
               .HasMaxLength(20)
               .IsRequired()
               .HasDefaultValueSql("CONCAT('ORDER-', FORMAT(NEXT VALUE FOR OrderNumberSequence, '00000'))");

            builder.HasIndex(o => o.OrderNumber)
                .IsUnique();

            //Configure enums
            builder.Property(o => o.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusOrder)Enum.Parse(typeof(StatusOrder), v)
                )
                .IsRequired();

            // Configure coordinate properties with appropriate precision
            builder.Property(o => o.DestinationLatitude)
                .HasPrecision(10, 8); // Appropriate for coordinates

            builder.Property(o => o.DestinationLongitude)
                .HasPrecision(11, 8);

            builder.Property(o => o.OriginLatitude)
                .HasPrecision(10, 8);

            builder.Property(o => o.OriginLongitude)
                .HasPrecision(11, 8);

            // Configure relationships with NoAction delete behavior
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Transporter)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TransporterId)
                .OnDelete(DeleteBehavior.NoAction);

            // One Order has one Package
            builder.HasOne(o => o.Package)
                  .WithOne(p => p.Order)
                  .HasForeignKey<Order>(o => o.PackageId);

            // One Order has one Bill
            builder.HasOne(o => o.Bill)
                  .WithOne(b => b.Order)
                  .HasForeignKey<Bill>(b => b.OrderId);

        }
    }
}