using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using ITPLibrary.Api.Data.Entities.Validation_Rules.Validation_Regex;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserRegisterStatus> RegisterUser(UserRegisterDto newUser);
        public Task<User> GetUser(UserLoginDto user);
        public Task<User> GetUser(string email);
        public bool PasswordAndConfirmedPasswordMatch(string password, string confirmedPassword);
        public bool IsPasswordValid(UserValidationRegex validation, string password);
        public bool IsEmailValid(UserValidationRegex validation, string email);
        public bool IsNameValid(UserValidationRegex validation, string name);
        public bool SendEmail(string emailBody, string email);
    }
}
