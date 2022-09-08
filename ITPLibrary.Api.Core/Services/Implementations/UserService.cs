using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.PasswordHasher;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly PasswordWithSaltHasher _passwordHasher;

        public UserService(IUserRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _passwordHasher = new PasswordWithSaltHasher();
        }

        public async Task<UserRegisterStatus> ValidateUserData(UserRegisterDto newUser)
        {
            if (await _repository.IsEmailAlreadyRegistered(newUser.Email))
            {
                return UserRegisterStatus.EmailAlreadyRegistered;
            }

            return UserRegisterStatus.Success;
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

        public async Task<bool> RegisterUser(UserRegisterDto newUser)
        {
            HashWithSaltResult hashResultSha256 = _passwordHasher
                   .HashWithSalt(newUser.Password, 64, SHA256.Create());

            var user = _mapper.Map<User>(newUser);

            user.Salt = hashResultSha256.Salt;
            user.HashedPassword = hashResultSha256.Digest;

            return await _repository.RegisterUser(user);
        }
    }
}
