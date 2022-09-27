using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Queries
{
    public class GetActiveRecoveryCodeQueryHandler : IRequestHandler<GetActiveRecoveryCodeQuery, RecoveryCode>
    {
        private readonly IRecoveryCodeRepository _recoveryCodeRepository;

        public GetActiveRecoveryCodeQueryHandler(IRecoveryCodeRepository recoveryCodeRepository)
        {
            _recoveryCodeRepository = recoveryCodeRepository;
        }

        public async Task<RecoveryCode> Handle(GetActiveRecoveryCodeQuery request, CancellationToken cancellationToken)
        {
            return await _recoveryCodeRepository.GetActiveRecoveryCode();
        }
    }
}
