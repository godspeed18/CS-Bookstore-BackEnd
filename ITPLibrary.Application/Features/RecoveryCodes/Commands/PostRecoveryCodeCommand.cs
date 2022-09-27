using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Commands
{
    public class PostRecoveryCodeCommand:IRequest<RecoveryCode>
    {
        public RecoveryCode RecoveryCode { get; set; }
    }
}
