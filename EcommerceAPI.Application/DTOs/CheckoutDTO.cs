using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Application.DTOs
{
    public class CheckoutDTO
    {
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        public AddressDTO BillingInformation { get; set; }
        [Required]
        public AddressDTO ShippingInformation { get; set; }
    }
}
