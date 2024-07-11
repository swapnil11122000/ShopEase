using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; } 

        [Required]

        public int OrderID { get; set; } 

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; } 

        public DateTime? PaymentDate { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; } 

        [MaxLength(100)]
        public string TransactionID { get; set; } 

        [Required]
        public PaymentStatus Status { get; set; } 

       
        public Order Order { get; set; }
    }
    public enum PaymentStatus
    {
        pending,
        completed,
        failed
    }
}
