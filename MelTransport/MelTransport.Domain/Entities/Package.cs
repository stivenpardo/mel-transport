using System.ComponentModel.DataAnnotations;

namespace MelTransport.Domain.Entities
{
    public class Package : BaseEntity
    {
        [Required]
        public string Name { get; private set; }

        [Required]
        public double Weigth { get; private set; }

        [MaxLength(100)]
        public string Description { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        // Navigation property
        public virtual Order Order { get; set; }

        private Package()
        {

        }

        public static Package Create(string name,
            double weight,
            string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            if (weight == 0)
                throw new ArgumentException("Weight cannot be empty");

            return new Package
            {
                Id = Guid.NewGuid(),
                Name = name,
                Weigth = weight,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdatePackage(
            string name, 
            double weight, 
            string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            if (weight == 0)
                throw new ArgumentException("Weight cannot be empty");

            Name = name;
            Weigth = weight;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
