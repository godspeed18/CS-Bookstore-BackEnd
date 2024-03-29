﻿using AutoMapper;
using Constants;
using ITPLibrary.Api.Core.Configurations;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.PasswordHasher;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Configurations;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.RequestStatuses;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly PasswordRecoveryConfiguration _passwordRecoveryConfiguration;
        private readonly PasswordWithSaltHasher _passwordHasher;
        private readonly PortAndHostConfiguration _portAndHostConfiguration;

        public UserService(IUserRepository repository,
                            IMapper mapper,
                                PasswordRecoveryConfiguration passwordRecoveryConfiguration,
                                    PortAndHostConfiguration portAndHostConfiguration)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordRecoveryConfiguration = passwordRecoveryConfiguration;
            _passwordHasher = new PasswordWithSaltHasher();
            _portAndHostConfiguration = portAndHostConfiguration;
        }

        public async Task<UserRegisterStatus> ValidateUserData(UserRegisterDto newUser)
        {
            if (await _repository.IsEmailAlreadyRegistered(newUser.Email))
            {
                return UserRegisterStatus.EmailAlreadyRegistered;
            }

            return UserRegisterStatus.Success;
        }

        public string GenerateRandomRecoveryCode()
        {
            RNG random = new RNG();
            return random.GenerateRandomCryptographicKey(10);
        }

        public async Task<bool> SendRecoveryEmail(string email, string emailBody)
        {
            try
            {
                MailMessage message = BuildMailMessage(emailBody, email);
                SmtpClient smtp = new SmtpClient();
                smtp.Port = Int32.Parse(_portAndHostConfiguration.SmtpPort);
                smtp.Host = _portAndHostConfiguration.SmtpHost;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_passwordRecoveryConfiguration.Email, _passwordRecoveryConfiguration.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        private MailMessage BuildMailMessage(string emailBody, string email)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_passwordRecoveryConfiguration.Email);
            message.To.Add(new MailAddress(email));
            message.Subject = CommonConstants.MessageSubject;
            message.IsBodyHtml = true;
            message.Body = emailBody;
            return message;
        }

        public async Task<bool> ChangePassword(int id, string password)
        {
            HashWithSaltResult hashResultSha256 = _passwordHasher
              .HashWithSalt(password, 64, SHA256.Create());

            return await _repository.ChangePassword(id, hashResultSha256.Digest, hashResultSha256.Salt);
        }
    }
}
