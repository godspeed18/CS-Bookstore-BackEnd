using System.Text.RegularExpressions;

namespace ITPLibrary.Api.Data.Entities.Validation_Rules.Validation_Regex
{
    public class UserValidationRegex
    {
        public Regex PasswordHasNumber = new Regex(@"[0-9]+");
        public Regex PasswordHasUpperChar = new Regex(@"[A-Z]+");
        public Regex PasswordHasMinimum8Chars = new Regex(@".{8,}");
        public Regex isEmailValid = new Regex("^\\S+@\\S+\\.\\S+$");
        public Regex isNameValid = new Regex("^[A-Za-z ]+$");
    }
}
