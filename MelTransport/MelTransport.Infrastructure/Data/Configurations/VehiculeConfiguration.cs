using MelTransport.Domain.Entities;
using MelTransport.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MelTransport.Infrastructure.Data.Configurations
{
    internal class VehiculeConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            //Configure enums
            builder.Property(v => v.VehiculeClass)
                .HasConversion(
                    v => v.ToString(),
                    v => (VehiculeClass)Enum.Parse(typeof(VehiculeClass), v)
                )
                .IsRequired();

            builder.Property(v => v.BodyType)
                .HasConversion(
                    v => v.ToString(),
                    v => (BodyTypeCar)Enum.Parse(typeof(BodyTypeCar), v)
                )
                .IsRequired();

            builder.Property(v => v.Service)
                .HasConversion(
                    v => v.ToString(),
                    v => (ServiceCar)Enum.Parse(typeof(ServiceCar), v)
                )
                .IsRequired();
        }
    }
}
