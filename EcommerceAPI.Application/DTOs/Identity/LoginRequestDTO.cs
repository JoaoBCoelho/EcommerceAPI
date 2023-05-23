using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Application.DTOs.Identity
{
    public class LoginRequestDTO
    {
        private const string mandatoryFieldErrorMessage = "The field {0} is mandatory.";

        [Required(ErrorMessage = mandatoryFieldErrorMessage)]
        [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = mandatoryFieldErrorMessage)]
        public string Password { get; set; }
    }
}
