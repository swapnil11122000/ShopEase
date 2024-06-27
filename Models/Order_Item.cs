using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Order_Item
    {
        [Key]
        [Required]
        [ScaffoldColumn(false)]
        public int Order_Item_ID { get; set; }

        [Required]
        public int Order_ID { get; set; }
        [Required]
        public int Product_ID { get; set; }
        [Required]
        public int Quantity {  get; set; }
        [Required]

        public int Unit_Price {  get; set; }
    }
}
