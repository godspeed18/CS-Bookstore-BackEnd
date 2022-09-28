using ITPLibrary.Application.Validation.ValidationConstants;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace ITPLibrary.Application.Features.Users.ViewModels
{
    public class SucessfulUserLoginVm
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
