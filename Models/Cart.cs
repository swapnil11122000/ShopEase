using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    [Table("Cart")]
    
    public class Cart
    {
        [Key]
        [ScaffoldColumn(false)]
        [Required]
        public int CartID { get; set; }
        [Required]
        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        [Required]

        public int UserID { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("UserID")]
        public virtual Users Users { get; set; }
    }



    
}
