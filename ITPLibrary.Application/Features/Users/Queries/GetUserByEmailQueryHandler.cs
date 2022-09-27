using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Queries
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByEmail(request.Email);
        }
    }
}
