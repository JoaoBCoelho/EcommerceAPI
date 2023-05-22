using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> AllExistAsync(IEnumerable<Guid> productIds);
        Task CreateAsync(Product product);
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetAsync();
        Task UpdateAsync(Product product);
    }
}
