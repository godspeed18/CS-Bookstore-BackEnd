using ITPLibrary.Api.Data.Entities.Validation_Rules;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace ITPLibrary.Api.Core.Dtos
{
    public class SuccessfulLoginDto
    {
        public int Id { get; set; } 

        [Required]
        [MaxLength(UserValidationRules.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(UserValidationRules.EmailMaxLength)]
        public string Email { get; set; }

        public JwtSecurityToken Token { get; set; }
    }
}
