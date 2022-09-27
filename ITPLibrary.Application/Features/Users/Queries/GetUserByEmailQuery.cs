using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<User>
    {
        public string Email { get; set; }
    }
}
