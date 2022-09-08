using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using ITPLibrary.Api.Data.Entities.Validation_Rules.Validation_Regex;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserRegisterStatus> ValidateUserData(UserRegisterDto newUser);
        public Task<bool> RegisterUser(UserRegisterDto newUser);
        public Task<User> GetUser(UserLoginDto user);
        public Task<User> GetUser(string email);
        public bool SendEmail(string emailBody, string email);
    }
}
