using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Commands
{
    public class PostRecoveryCodeCommandHandler : IRequestHandler<PostRecoveryCodeCommand, RecoveryCode>
    {
        private readonly IAsyncRepository<RecoveryCode> _recoveryCodeRepository;

        public PostRecoveryCodeCommandHandler(IAsyncRepository<RecoveryCode> recoveryCodeRepository)
        {
            _recoveryCodeRepository = recoveryCodeRepository;
        }

        public async Task<RecoveryCode> Handle(PostRecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            return await _recoveryCodeRepository.AddAsync(request.RecoveryCode);
        }
    }
}
