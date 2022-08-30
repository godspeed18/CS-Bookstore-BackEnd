using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using ITPLibrary.Api.Data.Entities.Validation_Rules.Validation_Regex;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public bool IsPasswordValid(UserValidationRegex validation, string password)
        {
            if (validation.PasswordHasMinimum8Chars.IsMatch(password) == false
                || validation.PasswordHasUpperChar.IsMatch(password) == false
                   || validation.PasswordHasNumber.IsMatch(password) == false)
            {
                return false;
            }

            return true;
        }

        public bool PasswordAndConfirmedPasswordMatch(string password, string confirmedPassword)
        {
            if (password == confirmedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEmailValid(UserValidationRegex validation, string email)
        {
            if (!validation.isEmailValid.IsMatch(email))
            {
                return false;
            }

            return true;
        }

        public bool IsNameValid(UserValidationRegex validation, string name)
        {
            if (!validation.isNameValid.IsMatch(name))
            {
                return false;
            }

            return true;
        }

        public bool SendEmail(string emailBody, string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_configuration["PasswordRecovery:Email"]);
                message.To.Add(new MailAddress(email));
                message.Subject = "Test";
                message.IsBodyHtml = true;
                message.Body = emailBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_configuration["PasswordRecovery:Email"], _configuration["PasswordRecovery:Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<User> GetUser(UserLoginDto user)
        {
            return await _repository.GetUser(user.Email, user.Password);
        }

        public async Task<User> GetUser(string email)
        {
            return await _repository.GetUser(email);
        }

        public async Task<UserRegisterStatus> RegisterUser(UserRegisterDto newUser)
        {
            return await _repository.RegisterUser(_mapper.Map<User>(newUser));
        }
    }
}
