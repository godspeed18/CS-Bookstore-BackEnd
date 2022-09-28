using AutoMapper;
using Constants;
using ITPLibrary.Api.Data.Configurations;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Users.ViewModels;
using ITPLibrary.Domain.Entites;
using ITPLibrary.PasswordHasher;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, SucessfulUserLoginVm>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly PasswordWithSaltHasher _passwordHasher;
        private readonly JwtConfiguration _jwtConfiguration;

        public LoginUserCommandHandler(IMapper mapper, IUserRepository userRepository, JwtConfiguration jwtConfiguration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtConfiguration = jwtConfiguration;
            _passwordHasher = new PasswordWithSaltHasher();
        }

        public async Task<SucessfulUserLoginVm> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await Login(request.UserLoginInfo);
        }

        public async Task<SucessfulUserLoginVm> Login(LoginUserVm user)
        {
            var userToBeLogged = await _userRepository.GetUserByEmail(user.Email);

            if (userToBeLogged != null
                && await IsPasswordCorrect(user.Password, userToBeLogged.Salt, user.Email))
            {
                Claim[] claims = GetClaims(userToBeLogged);
                var key = new SymmetricSecurityKey(Convert.FromBase64String(_jwtConfiguration.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = GetToken(claims, signIn);
                return CreateSuccessfulLoginDto(userToBeLogged, token);
            }

            return null;
        }

        private SucessfulUserLoginVm CreateSuccessfulLoginDto(User user, JwtSecurityToken token)
        {
            var userToBeLogged = _mapper.Map<SucessfulUserLoginVm>(user);
            userToBeLogged.Token = token;
            return userToBeLogged;
        }

        private JwtSecurityToken GetToken(Claim[] claims, SigningCredentials signIn)
        {
            return new JwtSecurityToken(
               _jwtConfiguration.Issuer,
               _jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: signIn);
        }

        private Claim[] GetClaims(User user)
        {
            return new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _jwtConfiguration.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString()),
                        new Claim(CommonConstants.IdClaim, user.Id.ToString()),
                        new Claim(CommonConstants.NameClaim, user.Name),
                        new Claim(CommonConstants.EmailClaim, user.Email)
            };
        }

        private async Task<bool> IsPasswordCorrect(string salt, string password, string email)
        {
            HashWithSaltResult hashResultSha256 = _passwordHasher
                   .HashWithSalt(password, salt, SHA256.Create());

            var user = await _userRepository.GetUserByEmail(email);

            return user.HashedPassword.Equals(hashResultSha256.Digest);
        }
    }
}
