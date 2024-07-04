using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; } 

        [Required]
        [MaxLength(100)]
        public string DiscountName { get; set; } 

        [Required]
        public DiscountType DiscountType { get; set; } 

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DiscountValue { get; set; } 

        [Required]
        public bool Status { get; set; } 

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MinPurchaseAmt { get; set; } 

        public DateTime? StartDate { get; set; } 

        public DateTime? EndDate { get; set; } 
    }
    public enum DiscountType
    {
        percentage,
        fixed_amount
    }
}
