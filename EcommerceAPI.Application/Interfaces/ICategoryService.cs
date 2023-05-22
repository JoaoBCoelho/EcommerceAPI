using EcommerceAPI.Application.DTOs;

namespace EcommerceAPI.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAsync();
    }
}
