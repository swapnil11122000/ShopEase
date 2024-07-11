using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; } 

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } 

        [MaxLength(200)]
        public string Description { get; set; } 

   
        public int CreatedBy { get; set; } 

        [Required]
        public DateTime CreatedDate { get; set; } 

        //public User User { get; set; }

    }
}
