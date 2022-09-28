using ITPLibrary.Application.Validation.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Application.Features.Users.ViewModels
{
    public class LoginUserVm
    {
        [Required]
        [MaxLength(UserValidationRules.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(UserValidationRules.PasswordMinLength)]
        [MaxLength(UserValidationRules.PasswordMaxLength)]
        public string Password { get; set; }
    }
}
