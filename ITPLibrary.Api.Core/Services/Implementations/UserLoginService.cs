using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.GenericConstants;
using ITPLibrary.Api.Core.PasswordHasher;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Configurations;
using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class UserLoginService : IUserLoginService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly PasswordWithSaltHasher _passwordHasher;
        private readonly JwtConfiguration _jwtConfiguration;

        public UserLoginService(ApplicationDbContext db, IMapper mapper, JwtConfiguration jwtConfiguration)
        {
            _db = db;
            _mapper = mapper;
            _passwordHasher = new PasswordWithSaltHasher();
            _jwtConfiguration = jwtConfiguration;
        }

        public async Task<SuccessfulLoginDto> Login(UserLoginDto user)
        {
            var userToBeLogged = await _db.Users.Where(u => u.Email == user.Email).FirstOrDefaultAsync();

            if (userToBeLogged != null
                && await IsPasswordCorrect(user, userToBeLogged.Salt))
            {
                Claim[] claims = GetClaims(userToBeLogged);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = GetToken(claims, signIn);
                return CreateSuccessfulLoginDto(userToBeLogged, token);
            }

            return null;
        }

        private SuccessfulLoginDto CreateSuccessfulLoginDto(User user, JwtSecurityToken token)
        {
            var userToBeLogged = _mapper.Map<SuccessfulLoginDto>(user);
            userToBeLogged.Token = token;
            return userToBeLogged;
        }

        private JwtSecurityToken GetToken(Claim[] claims, SigningCredentials signIn)
        {
            return new JwtSecurityToken(
               _jwtConfiguration.Issuer,
               _jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
        }

        private Claim[] GetClaims(User user)
        {
            return new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _jwtConfiguration.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString()),
                        new Claim(GenericConstant.IdClaim, user.Id.ToString()),
                        new Claim(GenericConstant.NameClaim, user.Name),
                        new Claim(GenericConstant.EmailClaim, user.Email)
            };
        }

        private async Task<bool> IsPasswordCorrect(UserLoginDto user, string salt)
        {
            HashWithSaltResult hashResultSha256 = _passwordHasher
                   .HashWithSalt(user.Password, salt, SHA256.Create());

            if (await _db.Users
                .Where(u => u.HashedPassword == hashResultSha256.Digest)
                    .FirstOrDefaultAsync() != null)
            {
                return true;
            }

            return false;
        }
    }
}
