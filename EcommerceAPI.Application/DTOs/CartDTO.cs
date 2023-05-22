namespace EcommerceAPI.Application.DTOs
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public bool IsCheckedOut { get; set; }
        public IEnumerable<CartProductDTO> Products { get; set; }
    }
}
