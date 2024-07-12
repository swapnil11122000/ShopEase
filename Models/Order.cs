using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; } 

        [Required]
      
        public int UserID { get; set; } 

        [Required]
        public DateTime OrderDate { get; set; } 

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; } 

        [Required]
        public string Status { get; set; } 

     
        public int? PaymentID { get; set; }

   
        public int? ShippingID { get; set; } 

      
        public User User { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
    }
   
}
