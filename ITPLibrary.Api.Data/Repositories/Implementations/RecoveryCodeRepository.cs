using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class RecoveryCodeRepository : IRecoveryCodeRepository
    {
        private readonly ApplicationDbContext _db;
        public RecoveryCodeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> GetActiveRecoveryCode(int userId)
        {
            int recoveryCodeId = await _db.RecoveryCodes.Where(u => u.UserId == userId && u.IsActive == true)
                                    .Select(u => u.Id).FirstOrDefaultAsync();
            return recoveryCodeId;
        }

        public async Task<bool> IsCodeCorrect(int userId, string code)
        {
            var result = await _db.RecoveryCodes.Where(u => u.UserId == userId && u.Code == code && u.IsActive == true).FirstOrDefaultAsync();
            return result != null;
        }

        public async Task<User> GetUser(string email)
        {
            return await _db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task PostRecoveryCode(string recoveryCode, string email, int userId)
        {
            RecoveryCode newCode = InitialiseRecoveryCode(userId, recoveryCode);
            await _db.RecoveryCodes.AddAsync(newCode);
            await _db.SaveChangesAsync();
        }

        public async Task SetRecoveryCodeNotValid(int recoveryCodeId)
        {
            var result = await _db.RecoveryCodes.SingleOrDefaultAsync(u => u.Id == recoveryCodeId && u.IsActive == true);
            result.IsActive = false;
            await _db.SaveChangesAsync();
        }

        public async Task SetRecoveryCodeNotValid(string recoveryCode)
        {
            var result = await _db.RecoveryCodes.SingleOrDefaultAsync(u => u.Code == recoveryCode && u.IsActive == true);
            result.IsActive = false;
            await _db.SaveChangesAsync();
        }

        private RecoveryCode InitialiseRecoveryCode(int userId, string recoveryCode)
        {
            RecoveryCode newCode = new RecoveryCode();
            newCode.UserId = userId;
            newCode.Code = recoveryCode;
            newCode.IsActive = true;

            return newCode;
        }
    }
}
