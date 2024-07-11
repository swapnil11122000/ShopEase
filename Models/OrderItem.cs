using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemsID { get; set; } 

        [Required]
       
        public int OrderID { get; set; } 

        [Required]
      
        public int ProductID { get; set; } 

        [Required]
        public int Quantity { get; set; } 

        public int? DiscountID { get; set; } 

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }

       
        public Order Order { get; set; }
        public Product Product { get; set; }
        public Discount Discount { get; set; }
    }
}
