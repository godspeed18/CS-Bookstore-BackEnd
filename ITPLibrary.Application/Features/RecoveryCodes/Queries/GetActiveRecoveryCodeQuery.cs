using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Queries
{
    public class GetActiveRecoveryCodeQuery : IRequest<RecoveryCode>
    {
    }
}
