using EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Interfaces
{
    public interface IProductService
    {
        Task AddRelatedProductAsync(Guid id, Guid relatedProductId);
        Task<Guid> CreateAsync(CreateProductDTO productDTO);
        Task<ProductDTO> GetAsync(Guid productId);
        Task<IEnumerable<ProductDTO>> GetAsync(ProductFilterDTO filterDto);
        Task RemoveRelatedProductAsync(Guid id, Guid relatedProductId);
    }
}
