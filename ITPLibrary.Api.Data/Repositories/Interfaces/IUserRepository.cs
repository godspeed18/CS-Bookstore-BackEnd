using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> RegisterUser(User newUser);
        public Task<User> GetUser(string email);
        public Task<bool> IsEmailAlreadyRegistered(string email);
        public Task<bool> ChangePassword(int id, string hashedPassword, string salt);
    }
}
