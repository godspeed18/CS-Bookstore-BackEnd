using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public interface RegisterUserCommand : IRequest<User>
    {
        public User User { get; set; }
    }
}
