using System.Text.Json.Serialization;

namespace EcommerceAPI.Application.DTOs.Identity
{
    public class LoginResponseDTO : BaseResponseDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ExpirationTime { get; private set; }

        public LoginResponseDTO() { }

        public LoginResponseDTO(bool success = true) : base(success) { }

        public LoginResponseDTO(bool success, string token, DateTime? expirationDate) : this(success)
        {
            Token = token;
            ExpirationTime = expirationDate;
        }

        public void AddError(string error) =>
            Errors.Add(error);
    }
}
