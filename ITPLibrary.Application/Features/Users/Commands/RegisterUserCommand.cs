using ITPLibrary.Application.Features.Users.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public class RegisterUserCommand : IRequest<User>
    {
        public RegisterUserVm User { get; set; }
    }
}
