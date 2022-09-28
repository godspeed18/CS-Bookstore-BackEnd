using ITPLibrary.Application.Features.Users.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public class LoginUserCommand : IRequest<SucessfulUserLoginVm>
    {
        public LoginUserVm UserLoginInfo { get; set; }
    }
}
