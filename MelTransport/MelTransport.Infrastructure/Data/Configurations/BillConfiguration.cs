using MelTransport.Domain.Entities;
using MelTransport.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelTransport.Infrastructure.Data.Configurations
{
    internal class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.Property(b => b.BillNumber)
                .HasMaxLength(20)
                .IsRequired()
                .HasDefaultValueSql("CONCAT('BILL-', FORMAT(NEXT VALUE FOR BillNumberSequence, '00000'))");

            builder.HasIndex(b => b.BillNumber)
               .IsUnique();

            //Configure enums
            builder.Property(b => b.StatusBill)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusBill)Enum.Parse(typeof(StatusBill), v)
                )
                .IsRequired();

            builder.Property(b => b.PayMethod)
                .HasConversion(
                    v => v.ToString(),
                    v => (PayMethod)Enum.Parse(typeof(PayMethod), v)
                )
                .IsRequired();

            // Configure decimal properties with precision
            builder.Property(b => b.Subtotal)
                .HasPrecision(18, 2); // 18 total digits, 2 decimal places

            builder.Property(b => b.TaxRate)
                .HasPrecision(5, 4); // Example: 5 total digits, 4 decimal places (e.g., 0.0999)

            builder.Property(b => b.TaxAmount)
                .HasPrecision(18, 2);

            builder.Property(b => b.DiscountAmount)
                .HasPrecision(18, 2);

            builder.Property(b => b.TotalAmount)
                .HasPrecision(18, 2);


            // Configure relationships with NoAction delete behavior
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bills)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Order)
                .WithOne(o => o.Bill)
                .HasForeignKey<Order>(b => b.BillId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Transporter)
                .WithMany(t => t.Bills)
                .HasForeignKey(b => b.TransporterId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}