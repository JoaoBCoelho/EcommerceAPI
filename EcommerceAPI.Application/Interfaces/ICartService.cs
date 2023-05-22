using EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(Guid id, Guid productId, int quantity);
        Task<Guid> CheckoutAsync(Guid id, CheckoutDTO checkoutDto);
        Task<Guid> CreateAsync();
        Task<CartDTO> GetAsync(Guid id);
        Task RemoveFromCartAsync(Guid id, Guid productId);
        Task UpdateCartAsync(Guid id, Guid productId, int quantity);
    }
}
