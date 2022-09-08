namespace ITPLibrary.Api.Data.Entities.RequestStatuses
{
    public enum UserRegisterStatus
    {
        Success,
        EmailNotValid,
        NameNotValid,
        WeakPassword,
        PasswordsDontMatch,
        EmailAlreadyRegistered
    }
}
