using EcommerceAPI.Application.DTOs.Identity;

namespace EcommerceAPI.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO user);
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO login);
    }
}
