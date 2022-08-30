namespace ITPLibrary.Api.Data.Entities.RequestStatuses
{
    public enum UserRegisterStatus
    {
        Success,
        NotValidEmail,
        WeakPassword,
        PasswordsDontMatch,
        EmailAlreadyRegistered
    }
}
