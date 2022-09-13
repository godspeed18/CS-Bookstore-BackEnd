namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IRecoveryCodeService
    {
        public Task<bool> PostRecoveryCode(string recoveryCode, string email);
        public Task<bool> IsCodeValid(int userId, string recoveryCode);
        public Task SetRecoveryCodeNotValid(string recoveryCode);
    }
}
