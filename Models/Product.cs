using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; } 

        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } 

        [MaxLength(150)]
        public string Description { get; set; } 

        [MaxLength(150)]
        public string ShortText { get; set; } 

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal UnitPrice { get; set; } 

        [Required]
   
        public int CategoryID { get; set; } 
        [Required]
     
        public int VendorID { get; set; } 

        public int StockQuantity { get; set; } 

        [MaxLength(100)]
       
        public string BarCode { get; set; } 

        [MaxLength(100)]
        public string ImgUrl { get; set; } 

        [Required]
        public bool Status { get; set; } 

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; } 

        
        public Category Category { get; set; }
        //public Vendor Vendor { get; set; }

    }
}
