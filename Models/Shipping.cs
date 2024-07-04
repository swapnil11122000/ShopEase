using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Shipping
    {
        [Key]
        public int ShippingID { get; set; } 

        [Required]
        [ForeignKey("Order")]
        public int OrderID { get; set; } 

        [Required]
        [MaxLength(100)]
        public string CarrierName { get; set; } 

        [Required]
        [MaxLength(50)]
        public string TrackingNumber { get; set; } 

        public DateTime ShippingDate { get; set; } 

        public DateTime EstimatedDeliveryDate { get; set; } 
        public DateTime? ActualDeliveryDate { get; set; } 
        [Required]
        public ShippingStatus Status { get; set; } 

        
        public Order Order { get; set; }
    }
    public enum ShippingStatus
    {
        processing,
        shipped,
        out_for_delivery,
        delivered,
        delayed
    }
}
