namespace EcommerceAPI.Application.DTOs
{
    public class NewOrderDTO : CheckoutDTO
    {
        public CartDTO Cart { get; set; }
    }
}
