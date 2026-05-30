using MelTransport.Domain.Enums;
using MelTransport.Domain.UserManage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelTransport.Domain.Entities
{
    public class Transporter : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public StatusTransporter Status { get; private set; }

        [Required]
        public decimal Wallet { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Vehicle))]
        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        //Navigation property (one Transporter has many orders)
        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        //Navigation property (one Transporter has many bills)
        private List<Bill> _bills = new();
        public IReadOnlyCollection<Bill> Bills => _bills.AsReadOnly();

        private Transporter() { }

        public static Transporter Create(
            StatusTransporter statusTransporter,
            decimal wallet,
            Guid VehiculeId)
        {
            if (statusTransporter == default)
                throw new ArgumentException("Status cannot be empty");

            if (wallet == default)
                throw new ArgumentException("Wallet cannot be empty");

            return new Transporter()
            {
                Id = Guid.NewGuid(),
                Status = statusTransporter,
                Wallet = wallet,
                VehicleId = VehiculeId,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public void UpdateStatus(StatusTransporter statusTransporter)
        {
            if (statusTransporter == default)
                throw new ArgumentException("Status cannot be empty");

            Status = statusTransporter;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateWallet(decimal amount)
        {
            Wallet += amount;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
