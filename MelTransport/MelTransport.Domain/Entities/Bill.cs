using MelTransport.Domain.Enums;
using MelTransport.Domain.UserManage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelTransport.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public string BillNumber { get; private set; }

        [Required]
        public StatusBill StatusBill { get; private set; }

        [Required]
        public DateTime CreateAt { get; private set; }

        [Required]
        public decimal Subtotal { get; private set; }

        [Required]
        public decimal TaxRate { get; private set; }

        [Required]
        public decimal TaxAmount { get; private set; }

        [Required]
        public decimal DiscountAmount { get; private set; }

        [Required]
        public decimal TotalAmount { get; private set; }

        [Required]
        public PayMethod PayMethod { get; private set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Transporter))]    
        public Guid TransporterId { get; set; }

        public Transporter Transporter { get; set; }

        private Bill() { }

        public static Bill Create(
            string billNumber,
            StatusBill statusBill, 
            PayMethod payMethod,
            decimal subtotal,
            decimal taxRate = 0, 
            decimal discountAmount = 0)
        {
            if(string.IsNullOrEmpty(billNumber))
                throw new ArgumentNullException("Bill number cannot be empty");

            if (statusBill == default)
                throw new ArgumentNullException("Status bill cannot be empty");

            if (payMethod == default)
                throw new ArgumentNullException("Pay method cannot be empty");

            if (subtotal == default)
                throw new ArgumentNullException("Subtotal cannot be empty");

            if (taxRate == default)
                throw new ArgumentNullException("Tax rate cannot be empty");

            if (discountAmount == default)
                throw new ArgumentNullException("Discount amount cannot be empty");

            var taxAmountOperation = subtotal * taxRate;
            
            return new Bill 
            {
                Id = Guid.NewGuid(),
                BillNumber = billNumber,
                StatusBill = statusBill,
                CreateAt = DateTime.UtcNow,
                PayMethod = payMethod,
                Subtotal = subtotal,
                TaxRate= taxRate ,
                TaxAmount = taxAmountOperation,
                DiscountAmount = discountAmount,
                TotalAmount = subtotal + taxAmountOperation - discountAmount,
            };
        }
    }
}
