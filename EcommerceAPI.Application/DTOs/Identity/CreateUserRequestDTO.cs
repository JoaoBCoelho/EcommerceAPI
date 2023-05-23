using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Application.DTOs.Identity
{
    public class CreateUserRequestDTO
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password and Confirmation must be equal.")]
        public string PasswordConfirmation { get; set; }
    }
}
