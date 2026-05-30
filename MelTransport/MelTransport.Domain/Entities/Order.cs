using MelTransport.Domain.Enums;
using MelTransport.Domain.UserManage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelTransport.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public string OrderNumber { get; private set; }

        [Required]
        public string Title { get; private set; }

        [Required]
        public StatusOrder Status { get; private set; }

        [Required]
        public decimal OriginLatitude { get; private set; }

        [Required]
        public decimal OriginLongitude { get; private set; }

        [Required]
        public decimal DestinationLatitude { get; private set; }

        [Required]
        public decimal DestinationLongitude { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Foreign key property
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        // Foreign key property
        [ForeignKey(nameof(Package))]
        public Guid PackageId { get; set; }

        public Package Package { get; set; }

        [ForeignKey(nameof(Transporter))]
        public Guid TransporterId { get; set; }
        public Transporter Transporter { get; set; }

        // Navigation properties
        public Bill Bill { get; set; }
        public Guid BillId { get; set; }

        public static Order Create(
            string orderNumber,
            string title,
            StatusOrder status,
            decimal originLatitude,
            decimal originLongitude,
            decimal destinationLatitude,
            decimal destinationLongitude)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
                throw new ArgumentException("Order number cannot be empty");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (status == default)
                throw new ArgumentException("Status cannot be empty");

            if (originLatitude == default)
                throw new ArgumentException("Origin Latitude cannot be empty");

            if (originLongitude == default)
                throw new ArgumentException("Origin Longitude cannot be empty");

            if (destinationLatitude == default)
                throw new ArgumentException("Destination Latitud cannot be empty");

            if (destinationLongitude == default)
                throw new ArgumentException("Destination Longitude cannot be empty");

            return new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = orderNumber,
                Title = title,
                Status = status,
                OriginLatitude = originLatitude,
                OriginLongitude = originLongitude,
                DestinationLatitude = destinationLatitude,
                DestinationLongitude = destinationLongitude,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateOrder(
            string title,
            StatusOrder status,
            decimal originLatitude,
            decimal originLongitude,
            decimal destinationLatitude,
            decimal destinationLongitude)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (status == default)
                throw new ArgumentException("Status cannot be empty");

            if (originLatitude == default)
                throw new ArgumentException("Origin Latitude cannot be empty");

            if (originLongitude == default)
                throw new ArgumentException("Origin Longitude cannot be empty");

            if (destinationLatitude == default)
                throw new ArgumentException("Destination Latitud cannot be empty");

            if (destinationLongitude == default)
                throw new ArgumentException("Destination Longitude cannot be empty");

            Title = title;
            Status = status;
            OriginLatitude = originLatitude;
            OriginLongitude = originLongitude;
            DestinationLatitude = destinationLatitude;
            DestinationLongitude = destinationLongitude;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(StatusOrder status)
        {
            if (status == default)
                throw new ArgumentException("Status cannot be empty");

            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
