using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; } 

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; } 

        [Required]
        public DateTime OrderDate { get; set; } 

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; } 

        [Required]
        public OrderStatus Status { get; set; } 

        [ForeignKey("Payment")]
        public int? PaymentID { get; set; }

        [ForeignKey("Shipping")]
        public int? ShippingID { get; set; } 

        // Navigation properties
        public User User { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
    }
    public enum OrderStatus
    {
        pending,
        confirmed,
        shipped,
        delivered,
        cancelled
    }
}
