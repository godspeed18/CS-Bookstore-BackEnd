using System.Text.RegularExpressions;

namespace ITPLibrary.Api.Data.Entities.Validation_Rules
{
    public static class UserValidationRules
    {
        public const int NameMaxLength = 60;
        public const int EmailMaxLength = 50;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 25;
    }
}
