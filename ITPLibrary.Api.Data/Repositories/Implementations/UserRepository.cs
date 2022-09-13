using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
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

        public async Task<bool> ChangePassword(int id, string hashedPassword, string salt)
        {
            try
            {
                var result = await _db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
                result.HashedPassword = hashedPassword;
                result.Salt = salt;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<User> GetUser(string email)
        {
            return await _db.Users.FirstOrDefaultAsync((u => u.Email == email));
        }

        //to be changed when i switch back to the user management branch
        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            var response = await _db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return response != null;
        }

        public async Task<bool> RegisterUser(User newUser)
        {
            if (await IsEmailAlreadyRegistered(newUser.Email) == false)
            {
                await _db.AddAsync(newUser);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}