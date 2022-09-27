using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Commands
{
    public class SetRecoveryCodeNotValidCommandHandler : IRequestHandler<SetRecoveryCodeNotValidCommand, RecoveryCode>
    {
        private readonly IRecoveryCodeRepository _recoveryCodeRepository;

        public SetRecoveryCodeNotValidCommandHandler(IRecoveryCodeRepository recoveryCodeRepository)
        {
            _recoveryCodeRepository = recoveryCodeRepository;
        }

        public async Task<RecoveryCode> Handle(SetRecoveryCodeNotValidCommand request, CancellationToken cancellationToken)
        {
            return await _recoveryCodeRepository.SetRecoveryCodeNotActive(request.RecoveryCodeId);
        }
    }
}
