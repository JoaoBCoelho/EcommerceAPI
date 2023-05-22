using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Application.DTOs
{
    public class CheckoutDTO
    {
        [Required]
        public string BillingAddress { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
