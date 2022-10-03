using ITPLibrary.Application.Contracts.Infrastructure;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using ITPLibrary.PasswordHasher;
using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Commands
{
    public class RequestPasswordRecoveryCommandHandler : IRequestHandler<RequestPasswordRecoveryCommand, string>
    {
        private readonly IRecoveryCodeRepository _recoveryCodeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public RequestPasswordRecoveryCommandHandler
            (
                IRecoveryCodeRepository recoveryCodeRepository,
                 IUserRepository userRepository,
                     IEmailService emailService
            )
        {
            _recoveryCodeRepository = recoveryCodeRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<string> Handle(RequestPasswordRecoveryCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.UserEmail);

            if (user == default)
            {
                return null;
            }

            var activeRecoveryCode = await _recoveryCodeRepository.GetActiveRecoveryCode(user.Id);

            if (activeRecoveryCode != default)
            {
                await _recoveryCodeRepository.SetRecoveryCodeNotActive(activeRecoveryCode.Id);
            }

            var recoveryCode = InitialiseRecoveryCode(code: GenerateRandomRecoveryCode(), userId: user.Id);
            await _recoveryCodeRepository.AddAsync(recoveryCode);

            _emailService.SendRecoveryEmail(email: request.UserEmail, emailBody: recoveryCode.Code);

            return request.UserEmail;
        }

        private string GenerateRandomRecoveryCode()
        {
            RNG random = new RNG();
            return random.GenerateRandomCryptographicKey(10);
        }

        private RecoveryCode InitialiseRecoveryCode(string code, int userId)
        {
            RecoveryCode recoveryCode = new RecoveryCode();

            recoveryCode.UserId = userId;
            recoveryCode.Code = code;
            recoveryCode.IsActive = true;

            return recoveryCode;
        }
    }
}
