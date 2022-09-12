namespace ITPLibrary.Api.Data.Entities.ErrorMessages
{
    public static class UserMessages
    {
        public const string Success = "Success";
        public const string EmailNotValid = "E-mail is not valid.";
        public const string NameNotValid = "Name is not valid.";
        public const string WeakPassword = "Password is too weak.";
        public const string PasswordsDontMatch = "Password and confirmed password do not match.";
        public const string EmailAlreadyRegistered = "An account with this e-mail already exists.";
        public const string RecoveryEmailSent = "E-mail was successfully sent.";
        public const string RecoveryEmailNotSent = "Oops... An error occured. The password recovery e-mail was not sent.";
    }
}
