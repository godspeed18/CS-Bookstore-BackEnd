using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class UserRepository : BaseAsyncRepository<User>, IUserRepository
    {
        public UserRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> DoesPasswordExistInDb(string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
