using MelTransport.Domain.Entities;
using MelTransport.Domain.Enums;
using MelTransport.Domain.UserManage.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace MelTransport.Domain.UserManage
{
    public class User : BaseEntity
    {
        [Required]
        public TypeId TypeId { get; private set; }

        [Required]
        public int IdentificationNumber { get; private set; }

        [Required]
        public string Address { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; private set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; private set; }

        [Required]
        public PasswordHash PasswordHash { get; private set; }

        [Required]
        public UserType UserType { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        //Navigation property (one user has many orders)
        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        //Navigation property (one user has many bills)
        private List<Bill> _bills = new();
        public IReadOnlyCollection<Bill> Bills => _bills.AsReadOnly();

        //Navigation property (one user has one transporter)
        public Transporter Transporter { get; set; }
        public Guid TransporterId { get; set; }

        private User() { }

        public static User Create(
            TypeId typeId,
            int identificationNumber,
            string address,
            string email,
            string firstName,
            string lastName,
            PasswordHash password,
            UserType userType)
        {

            if (identificationNumber == default)
                throw new ArgumentException("Identification Number cannot be empty");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be empty");

            if (string.IsNullOrEmpty(email) || !email.Contains('@'))
                throw new ArgumentException("Invalid email address");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty");


            return new User
            {
                Id = Guid.NewGuid(),
                TypeId = typeId,
                IdentificationNumber = identificationNumber,
                Address = address,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = password,
                UserType = userType,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty");

            FirstName = firstName;
            LastName = lastName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email address");

            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(PasswordHash newPassword)
        {
            PasswordHash = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
            UpdatedAt = DateTime.UtcNow;
        }

        // Domain methods
        public void AddPackage(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            _orders.Add(order);
        }

        public void AddBill(Bill bill)
        {
            if (bill == null) throw new ArgumentNullException(nameof(bill));
            _bills.Add(bill);
        }
    }
}
