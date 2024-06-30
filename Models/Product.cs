using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
       
        public int CategoryID { get; set; }

        [Required]
        public int Stock_Quantity { get; set; }

        [Required]
        public int Price { get; set; }
        [Required]
        public string Short_Text { get; set; }

        public string  Long_Text { get; set; }
        public int Min_Order_Qty { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public int Vendor_ID { get; set; }

        public virtual Category Category { get; set; }

    }
}
