using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Contracts.Persistance
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<bool> DoesPasswordExistInDb(string password);
    }
}
