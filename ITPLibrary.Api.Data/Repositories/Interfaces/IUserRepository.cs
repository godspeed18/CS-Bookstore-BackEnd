using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserRegisterStatus> RegisterUser(User newUser);
        public Task<User> GetUser(string email, string password);
        public Task<User> GetUser(string email);
    }
}
