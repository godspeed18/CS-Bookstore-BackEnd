using ITPLibrary.Api.Data.Entities.Validation_Rules;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Api.Core.Dtos
{
    public class UserRegisterDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(UserValidationRules.NameMaxLength)]
        public string Name { get; set; }

        [Required] 
        [MaxLength(UserValidationRules.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(UserValidationRules.PasswordMinLength)]
        [MaxLength(UserValidationRules.PasswordMaxLength)]
        public string Password { get; set; }

        [Required]
        [MinLength(UserValidationRules.PasswordMinLength)]
        [MaxLength(UserValidationRules.PasswordMaxLength)]
        public string ConfirmedPassword { get; set; }

    }
}
