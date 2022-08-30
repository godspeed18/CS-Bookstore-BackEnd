using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IUserLoginService
    {
        public Task<SuccessfulLoginDto> Login(User loginUser);
    }
}
