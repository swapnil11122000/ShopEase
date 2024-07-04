using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; } 

        [Required]
        public EntityType EntityType { get; set; } 

        [Required]
        public int EntityID { get; set; } 

        [Required]
        [EnumDataType(typeof(AddressType))]
        public AddressType AddressType { get; set; } 

        [Required]
        [MaxLength(100)]
        public string LandMark { get; set; } 

        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; } 

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
    }
    public enum EntityType
    {
        User,
        Vendor
    }

    public enum AddressType
    {
        Shipping,
        Billing,
        Other
    }
}
