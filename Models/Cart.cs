using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    [Table("Cart")]
    
    public class Cart
    {
        [Key]
        public int CartID { get; set; } 

        [Required]
     
        public int UserID { get; set; } 

        [Required]
     
        public int ProductID { get; set; } 

        [Required]
        public int Quantity { get; set; } 

   
        public int? DiscountID { get; set; } 

        [Required]
        public DateTime CreatedDate { get; set; } 

        public DateTime? UpdatedDate { get; set; } 

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; } 

        
        public User User { get; set; }
        public Product Product { get; set; }
        public Discount Discount { get; set; }
    }



    
}
