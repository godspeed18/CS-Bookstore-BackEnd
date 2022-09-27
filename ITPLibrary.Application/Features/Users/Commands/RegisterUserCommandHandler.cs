using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<User> _userRepository;

        public RegisterUserCommandHandler(IMapper mapper, IAsyncRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.AddAsync(request.User);
        }
    }
}
