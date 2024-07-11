using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    public class Return
    {
        [Key]
        public int ReturnID { get; set; } 
        [Required]
   
        public int OrderID { get; set; }

        [MaxLength(500)]
        public string ReturnReason { get; set; } 

        [Required]
        public DateTime ReturnDate { get; set; } 
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal RefundAmount { get; set; } 

        // Navigation property
        public Order Order { get; set; }
    }
}
