using System.Net.Mail;

namespace ITPLibrary.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        public bool SendRecoveryEmail(string email, string emailBody);
        public MailMessage BuildMailMessage(string emailBody, string email);

    }
}
