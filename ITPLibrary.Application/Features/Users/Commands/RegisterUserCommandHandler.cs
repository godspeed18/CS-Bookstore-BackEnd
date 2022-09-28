using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using ITPLibrary.PasswordHasher;
using MediatR;
using System.Security.Cryptography;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserByEmail(request.User.Email);
            if (user != null)
            {
                return null;
            }
            
            PasswordWithSaltHasher passwordHasher = new PasswordWithSaltHasher();
            HashWithSaltResult hashResultSha256 = passwordHasher
                   .HashWithSalt(request.User.Password, 64, SHA256.Create());

            var mappedUser = _mapper.Map<User>(request.User);
            mappedUser.Salt = hashResultSha256.Salt;
            mappedUser.HashedPassword = hashResultSha256.Digest;
            var newUser = await _userRepository.AddAsync(mappedUser);

            await _userRepository.SaveChangesAsync();
            return newUser;
        }
    }
}
