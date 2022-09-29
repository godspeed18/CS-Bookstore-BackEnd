using MediatR;

namespace ITPLibrary.Application.Features.RecoveryCodes.Commands
{
    public class RequestPasswordRecoveryCommand: IRequest<string>
    {
        public string UserEmail { get; set; }
    }
}
