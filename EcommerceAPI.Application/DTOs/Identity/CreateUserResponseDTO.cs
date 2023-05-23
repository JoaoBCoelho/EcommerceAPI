namespace EcommerceAPI.Application.DTOs.Identity
{
    public class CreateUserResponseDTO : BaseResponseDTO
    {
        public CreateUserResponseDTO(bool success = true) : base(success) { }

        public void AddErrors(IEnumerable<string> errors) =>
            Errors.AddRange(errors);
    }
}
