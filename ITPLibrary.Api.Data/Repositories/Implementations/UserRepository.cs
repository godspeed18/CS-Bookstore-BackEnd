using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _db.Users.FirstOrDefaultAsync((u => u.Email == email && u.Password == password));
        }

        public async Task<User> GetUser(string email)
        {
            return await _db.Users.FirstOrDefaultAsync((u => u.Email == email));
        }

        //to be changed when i switch back to the user management branch
        public async Task<UserRegisterStatus> RegisterUser(User newUser)
        {
            string email = _db
                         .Users
                         .Where(u => u.Email == newUser.Email)
                         .Select(u => u.Email)
                         .FirstOrDefault();

            if (email == null)
            {
                await _db.AddAsync(newUser);
                await _db.SaveChangesAsync();
                return UserRegisterStatus.Success;
            }

            return UserRegisterStatus.EmailAlreadyRegistered;
        }
    }
}