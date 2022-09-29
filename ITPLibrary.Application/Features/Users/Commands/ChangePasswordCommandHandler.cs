using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using ITPLibrary.PasswordHasher;
using MediatR;
using System.Security.Cryptography;

namespace ITPLibrary.Application.Features.Users.Commands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly IRecoveryCodeRepository _recoveryCodeRepository;
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IRecoveryCodeRepository recoveryCodeRepository, IUserRepository userRepository)
        {
            _recoveryCodeRepository = recoveryCodeRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.PasswordReset.Email);
            if (user == null || request.PasswordReset.RecoveryCode == null)
            {
                return null;
            }

            var recoveryCode = await _recoveryCodeRepository.GetActiveRecoveryCode(user.Id);
            if (recoveryCode.Code.Equals(request.PasswordReset.RecoveryCode) == false)
            {
                return null;
            }

            await _recoveryCodeRepository.SetRecoveryCodeNotActive(recoveryCode.Id);
            await ChangeUserPasswordAsync(user, request.PasswordReset.Password);
            return request.PasswordReset.RecoveryCode;
        }

        private async Task<User> ChangeUserPasswordAsync(User user, string password)
        {
            PasswordWithSaltHasher passwordHasher = new PasswordWithSaltHasher();
            HashWithSaltResult hashResultSha256 = passwordHasher
            .HashWithSalt(password, 64, SHA256.Create());

            user.HashedPassword = hashResultSha256.Digest;
            user.Salt = hashResultSha256.Salt;

            var updatedUser = await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return updatedUser;
        }
    }
}
