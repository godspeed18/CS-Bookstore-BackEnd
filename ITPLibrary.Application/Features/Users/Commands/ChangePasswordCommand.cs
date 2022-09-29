using ITPLibrary.Application.Features.Users.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public class ChangePasswordCommand: IRequest<string>
    {
        public PasswordRecoveryVm PasswordReset { get; set; }
    }
}
