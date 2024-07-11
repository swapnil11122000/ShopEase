using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; } 

        [Required]
     
        public int ProductID { get; set; } 
        [Required]
     
        public int UserID { get; set; } 
        [Range(1, 5)]
        public byte Rating { get; set; } 

        [MaxLength(500)]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; } 

       
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
