namespace ITPLibrary.Api.Data.Entities.Validation_Rules.Validation_Regex
{
    public static class UserValidationRegex
    { 
        //a minimum length of 8 characters, one upper, one lower, one special, one numeric character
        public const string isPasswordValid= @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"; 
       
        public const string isEmailValid = "^\\S+@\\S+\\.\\S+$";
        
        public const string isNameValid = "^[A-Za-z ]+$";
    }
}
