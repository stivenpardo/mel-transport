using MelTransport.Domain.Entities;
using MelTransport.Domain.Enums;
using MelTransport.Domain.UserManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelTransport.Infrastructure.Data.Configurations
{
    internal class TransporterConfiguration : IEntityTypeConfiguration<Transporter>
    {
        public void Configure(EntityTypeBuilder<Transporter> builder)
        {
            //Configure relationships
            builder.HasOne(t => t.Vehicle)
              .WithOne(v => v.Transporter)
              .HasForeignKey<Transporter>(t => t.VehicleId);

            // One Transporter has many Order
            builder.HasMany(t => t.Orders)
                  .WithOne(o => o.Transporter)
                  .HasForeignKey(o => o.TransporterId);

            // One Transporter has many Bill
            builder.HasMany(t => t.Bills)
                  .WithOne(b => b.Transporter)
                  .HasForeignKey(b => b.TransporterId);

            //Configure enums
            builder.Property(t => t.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusTransporter)Enum.Parse(typeof(StatusTransporter), v)
                   )
                .IsRequired();

            builder.Property(t => t.Wallet)
                .HasPrecision(18, 2); // Monetary value

            //Configure relationships with NoAction delete behavior
            builder.HasOne(t => t.User)
                .WithOne(u => u.Transporter)
                .HasForeignKey<User>(u => u.TransporterId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
