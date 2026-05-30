using MelTransport.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MelTransport.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public int TransitLicence { get; set; }

        [Required]
        [MaxLength(100)]
        public string LicencePlate { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Brand { get; private set; }

        [Required]
        [MaxLength(100)]
        public int Model { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Color { get; private set; }

        [Required]
        [MaxLength(100)]
        public VehiculeClass VehiculeClass { get; private set; }

        [Required]
        [MaxLength(100)]
        public BodyTypeCar BodyType { get; private set; }

        [Required]
        [MaxLength(100)]
        public ServiceCar Service { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Vin { get; private set; }

        [Required]
        public DateTime ExpireSoat { get; private set; }

        [Required]
        public DateTime ExpireMechanicalTechnical { get; private set; }

        // Navigation property
        public virtual Transporter Transporter { get; set; }

        private Vehicle()
        {

        }

        public static Vehicle Create(
            int transitLicence,
            string licencePlate,
            string brand,
            int model,
            string color,
            VehiculeClass vehiculeClass,
            BodyTypeCar bodyType,
            ServiceCar service,
            string vin,
            DateTime expireSoat,
            DateTime expireMechanicalTechnical)
        {
            if (transitLicence == default)
                throw new ArgumentException("Transit licence cannot be empty");

            if (string.IsNullOrEmpty(licencePlate))
                throw new ArgumentException("Licence plate cannot be empty");

            if (string.IsNullOrEmpty(brand))
                throw new ArgumentException("Brand cannot be empty");

            if (model == default)
                throw new ArgumentException("Model cannot be empty");

            if (string.IsNullOrEmpty(color))
                throw new ArgumentException("Color cannot be empty");

            if (vehiculeClass == default)
                throw new ArgumentException("Vehicle class cannot be empty");

            if (service == default)
                throw new ArgumentException("Service car cannot be empty");

            if (string.IsNullOrEmpty(vin))
                throw new ArgumentException("VIN cannot be empty");

            if (expireSoat == default)
                throw new ArgumentException("Expire soat cannot be empty");

            if (expireMechanicalTechnical == default)
                throw new ArgumentException("Expire Mechanical Technical cannot be empty");

            return new Vehicle()
            {
                Id = Guid.NewGuid(),
                TransitLicence = transitLicence,
                LicencePlate = licencePlate,
                Brand = brand,
                Model = model,
                Color = color,
                VehiculeClass = vehiculeClass,
                BodyType = bodyType,
                Service = service,
                Vin = vin,
                ExpireSoat = expireSoat,
                ExpireMechanicalTechnical = expireMechanicalTechnical
            };
        }

        public void Update(
            int transitLicence,
            string licencePlate,
            string brand,
            int model,
            string color,
            VehiculeClass vehiculeClass,
            BodyTypeCar bodyType,
            ServiceCar service,
            string vin,
            DateTime expireSoat,
            DateTime expireMechanicalTechnical)
        {
            if (transitLicence == default)
                throw new ArgumentException("Transit licence cannot be empty");

            if (string.IsNullOrEmpty(licencePlate))
                throw new ArgumentException("Licence plate cannot be empty");

            if (string.IsNullOrEmpty(brand))
                throw new ArgumentException("Brand cannot be empty");

            if (model == default)
                throw new ArgumentException("Model cannot be empty");

            if (string.IsNullOrEmpty(color))
                throw new ArgumentException("Color cannot be empty");

            if (vehiculeClass == default)
                throw new ArgumentException("Vehicle class cannot be empty");

            if (service == default)
                throw new ArgumentException("Service car cannot be empty");

            if (string.IsNullOrEmpty(vin))
                throw new ArgumentException("VIN cannot be empty");

            if (expireSoat == default)
                throw new ArgumentException("Expire soat cannot be empty");

            if (expireMechanicalTechnical == default)
                throw new ArgumentException("Expire Mechanical Technical cannot be empty");


            TransitLicence = transitLicence;
            LicencePlate = licencePlate;
            Brand = brand;
            Model = model;
            Color = color;
            VehiculeClass = vehiculeClass;
            BodyType = bodyType;
            Service = service;
            Vin = vin;
            ExpireSoat = expireSoat;
            ExpireMechanicalTechnical = expireMechanicalTechnical;
        }
    }
}
