using MelTransport.Domain.Entities;
using MelTransport.Domain.Enums;
using MelTransport.Domain.UserManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelTransport.Infrastructure.UserManage.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Configure relationships
            // One User has one Transporter
            builder.HasOne(u => u.Transporter)
                  .WithOne(t => t.User)
                  .HasForeignKey<Transporter>(t => t.UserId);

            // One User has many Order
            builder.HasMany(u => u.Orders)
                  .WithOne(o => o.User)
                  .HasForeignKey(o => o.UserId);

            // One User has many Bill
            builder.HasMany(u => u.Bills)
                  .WithOne(b => b.User)
                  .HasForeignKey(b => b.UserId);

            //Configure Value object
            builder.OwnsOne(u => u.PasswordHash, p =>
            {
                p.Property(pw => pw.Hash)
                .HasColumnName("PasswordHash")
                .IsRequired();

                p.Property(pw => pw.Salt)
                .HasColumnName("PasswordSalt")
                .IsRequired();
            });


            //Configure enums
            builder.Property(u => u.TypeId)
                .HasConversion(
                    v => v.ToString(),
                    v => (TypeId)Enum.Parse(typeof(TypeId), v))
                .IsRequired();

            builder.Property(u => u.UserType)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserType)Enum.Parse(typeof(UserType), v)
                )
                .IsRequired();

            //Index 
            builder.HasIndex(u => new { u.TypeId, u.IdentificationNumber })
                .IsUnique();

            builder.HasIndex( u => u.Email)
                .IsUnique();
        }
    }
}
