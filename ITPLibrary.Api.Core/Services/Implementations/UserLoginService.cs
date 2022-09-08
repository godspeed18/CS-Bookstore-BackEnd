using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IConfiguration _configuration;

        public UserLoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SuccessfulLoginDto> Login(User user)
        {
            Claim[] claims = GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = GetToken(claims, signIn);
            return CreateUser(user, token);
        }

        private static SuccessfulLoginDto CreateUser(User user, JwtSecurityToken token)
        {
            return new SuccessfulLoginDto()
            {
                Email = user.Email,
                Password = user.HashedPassword,
                Name = user.Name,
                Token = token
            };
        }

        private JwtSecurityToken GetToken(Claim[] claims, SigningCredentials signIn)
        {
            return new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
        }

        private Claim[] GetClaims(User user)
        {
            return new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("Password", user.HashedPassword),
                        new Claim("Email", user.Email)
            };
        }
    }
}
