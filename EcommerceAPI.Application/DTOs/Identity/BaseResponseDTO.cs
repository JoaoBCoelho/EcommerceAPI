namespace EcommerceAPI.Application.DTOs.Identity
{
    public abstract class BaseResponseDTO
    {
        public BaseResponseDTO()
        {
            Errors = new List<string>();
        }

        public BaseResponseDTO(bool success) : this()
        {
            Success = success;
        }

        public List<string> Errors { get; private set; }
        public bool Success { get; }
    }
}
