using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Repositories.Interfaces;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class RecoveryCodeService : IRecoveryCodeService
    {
        private readonly IRecoveryCodeRepository _repository;

        public RecoveryCodeService(IRecoveryCodeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsCodeValid(int userId, string recoveryCode)
        {
            return await _repository.IsCodeCorrect(userId, recoveryCode);
        }

        public async Task<bool> PostRecoveryCode(string recoveryCode, string email)
        {
            var user = await _repository.GetUser(email);

            if (user != default)
            {
                int activeRecoveryCodeId = await _repository.GetActiveRecoveryCode(user.Id);

                if (activeRecoveryCodeId != default)
                {
                    await _repository.SetRecoveryCodeNotValid(activeRecoveryCodeId);
                }

                await _repository.PostRecoveryCode(recoveryCode, email, user.Id);
                return true;
            }

            return false;
        }

        public async Task SetRecoveryCodeNotValid(string recoveryCode)
        {
            await _repository.SetRecoveryCodeNotValid(recoveryCode);
        }
    }
}
