using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IRecoveryCodeRepository
    {
        public Task PostRecoveryCode(string recoveryCode, string email, int userId);
        public Task<int> GetActiveRecoveryCode(int userId);
        public Task SetRecoveryCodeNotValid(string recoveryCode);
        public Task<bool> IsCodeCorrect(int userId, string code);
        public Task<User> GetUser(string email);
        public Task SetRecoveryCodeNotValid(int recoveryCodeId);
    }
}
