using EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> GetAsync(Guid id);
    }
}
