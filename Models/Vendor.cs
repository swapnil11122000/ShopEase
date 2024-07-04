using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        public int VendorID { get; set; } 

        public int? UserID { get; set; } 

        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; } 

        [MaxLength(200)]
        public string ContactPerson { get; set; } 

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } 

        [Required]
        [MaxLength(150)]
        public string Password { get; set; } 

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } 

        [Required]
        [ForeignKey("Address")]
        public int AddressID { get; set; } 
        [Required]
        public int? GSTINID { get; set; } 

        [Required]
        public DateTime CreatedDate { get; set; } 

        public DateTime? UpdatedDate { get; set; }
        [Required]
        public int Status { get; set; }

        public Address Address { get; set; }

        
        public User User { get; set; }



    }
}
