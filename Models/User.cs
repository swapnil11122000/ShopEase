using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ECommWeb.Models
{
    public class User
    {
        [Key]
        
        public int UserID { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        
        [EmailAddress]
        public string Email { get; set; }
        [Required]
       
        public string Password { get; set; }
        [Required]
        [MaxLength(20)]
        public string Mobile { get; set; }
       
        public bool Status { get; set; }

        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastLogin { get; set; }

        [Required]
      
        public int ProfileID { get; set; }

        [Required]
    
        public int AddressID { get; set; }

        public Profile Profile { get; set; }
        public Address Address { get; set; }

    }
}
