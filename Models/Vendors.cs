using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommWeb.Models
{
    [Table("Vendors")]
    public class Vendors
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Vendor_ID { get; set; }
        [Required]
        public string Vendor_Name { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string  Address2 { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Phone_Number { get; set; }
        [Required]
        public bool IsActive { get; set; }



    }
}
