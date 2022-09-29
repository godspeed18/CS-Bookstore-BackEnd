using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Contracts.Persistance
{
    public interface IRecoveryCodeRepository : IAsyncRepository<RecoveryCode>
    {
        public Task<RecoveryCode> SetRecoveryCodeNotActive(int recoveryCodeId);
        public Task<RecoveryCode> GetActiveRecoveryCode(int userId);
    }
}
