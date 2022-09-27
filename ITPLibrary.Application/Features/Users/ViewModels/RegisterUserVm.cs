using ITPLibrary.Api.Data.Entities.ErrorMessages;
using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Application.Validation.ValidationRegex;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Application.Features.Users.ViewModels
{
    public class RegisterUserVm
    {
        [Required]
        [MaxLength(UserValidationRules.NameMaxLength)]
        [RegularExpression(UserValidationRegex.isNameValid, ErrorMessage = UserMessages.NameNotValid)]
        public string Name { get; set; }

        [Required]
        [MaxLength(UserValidationRules.EmailMaxLength)]
        [RegularExpression(UserValidationRegex.isEmailValid, ErrorMessage = UserMessages.EmailNotValid)]
        public string Email { get; set; }

        [Required]
        [MinLength(UserValidationRules.PasswordMinLength)]
        [MaxLength(UserValidationRules.PasswordMaxLength)]
        [RegularExpression(UserValidationRegex.isPasswordValid, ErrorMessage = UserMessages.WeakPassword)]
        public string Password { get; set; }

        [Required]
        [MinLength(UserValidationRules.PasswordMinLength)]
        [MaxLength(UserValidationRules.PasswordMaxLength)]
        public string ConfirmedPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (this.Password != this.ConfirmedPassword)
            {
                results.Add(new ValidationResult(UserMessages.PasswordsDontMatch));
            }

            return results;
        }
    }
}
