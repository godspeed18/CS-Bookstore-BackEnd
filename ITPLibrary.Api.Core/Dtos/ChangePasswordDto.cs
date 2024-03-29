﻿using ITPLibrary.Api.Data.Entities.ErrorMessages;
using ITPLibrary.Api.Data.Entities.Validation_Rules;
using ITPLibrary.Api.Data.Entities.Validation_Rules.Validation_Regex;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Api.Core.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string RecoveryCode { get; set; }

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
