using ITPLibrary.Api.Data.Entities.Validation_Rules;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace ITPLibrary.Api.Core.Dtos
{
    public class UserLoginDto
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
