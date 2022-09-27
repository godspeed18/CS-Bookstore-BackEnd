using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Commands
{
    public class SetRecoveryCodeNotValidCommand : IRequest<RecoveryCode>
    {
        public int RecoveryCodeId { get; set; }
    }
}
