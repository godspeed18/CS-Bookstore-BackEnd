using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IUserService
    {
        public string GenerateRandomRecoveryCode();
        public Task<UserRegisterStatus> ValidateUserData(UserRegisterDto newUser);
        public Task<bool> RegisterUser(UserRegisterDto newUser);
        public Task<User> GetUser(string email);
        public Task<bool> SendRecoveryEmail(string email, string emailBody);
        public Task<bool> ChangePassword(int userId, string newPassword);
    }
}
