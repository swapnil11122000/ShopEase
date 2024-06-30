using System.ComponentModel.DataAnnotations;
namespace ECommWeb.Models
{
    public class Users
    {
        [Key]
        [ScaffoldColumn(false)]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public  string Email { get; set; }
               
        public int Mobile { get; set; }
       
        public string Gender { get; set; }

        public string Address { get; set; }
      
        public bool Role {  get; set; }

        public bool IsActive {  get; set; }
    }
}
