using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Profile
    {
        [Key]
        public int ProfileID { get; set; } 

        [Required]
        [MaxLength(50)]
        public string ProfileName { get; set; } 

        [MaxLength(50)]
        public string Description { get; set; } 

        [Required]
        [ForeignKey("User")]
        public int CreatedBy { get; set; } 

        [Required]
        public DateTime CreatedDate { get; set; } 

        
        public User User { get; set; }
    }
}
