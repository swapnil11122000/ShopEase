using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Order
    {
        [Key]
        [Required]
        [ScaffoldColumn(false)]
        public int Order_ID { get; set; }
        [Required]
        public int User_ID { get; set; }
        [Required
            ]
        public DateTime Order_Date { get; set; }
        [Required]

        public int Total_Amount {  get; set; }
        [Required]

        public string Status {  get; set; }

    }
}
