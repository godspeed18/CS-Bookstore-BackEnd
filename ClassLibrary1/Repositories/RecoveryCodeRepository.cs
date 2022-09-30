using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class RecoveryCodeRepository : BaseAsyncRepository<RecoveryCode>, IRecoveryCodeRepository
    {
        public RecoveryCodeRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RecoveryCode> GetActiveRecoveryCode(int userId)
        {
           return await _db.RecoveryCodes.Where(u => u.UserId == userId && u.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<RecoveryCode> SetRecoveryCodeNotActive(int recoveryCodeId)
        {
            var result = await _db.RecoveryCodes.SingleOrDefaultAsync(u => u.Id == recoveryCodeId && u.IsActive == true);
            result.IsActive = false;
            await _db.SaveChangesAsync();

            return result;
        }
    }
}
