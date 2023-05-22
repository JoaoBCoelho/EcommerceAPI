using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Application.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string CustomerEmail { get; set; }
        public AddressResponseDTO BillingInformation { get; set; }
        public AddressResponseDTO ShippingInformation { get; set; }
        public CartDTO Cart { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
