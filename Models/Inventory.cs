using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; } 

        [Required]
        [ForeignKey("Product")]
       
        public int ProductID { get; set; } 

        [Required]
        public int StockQuantity { get; set; } 

        public DateTime LastUpdated { get; set; } 

       
        public Product Product { get; set; }
    }
}
