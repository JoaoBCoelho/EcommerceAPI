using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task<Order> GetAsync(Guid id);
    }
}
