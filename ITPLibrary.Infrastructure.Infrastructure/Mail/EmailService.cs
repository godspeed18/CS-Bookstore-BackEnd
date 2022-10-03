using Constants;
using ITPLibrary.Application.Contracts.Infrastructure;
using ITPLibrary.Common;
using System.Net;
using System.Net.Mail;

namespace ITPLibrary.Infrastructure.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly PortAndHostConfiguration _portAndHostConfiguration;
        private readonly PasswordRecoveryConfiguration _passwordRecoveryConfiguration;

        public EmailService(PasswordRecoveryConfiguration passwordRecoveryConfiguration, PortAndHostConfiguration portAndHostConfiguration)
        {
            _passwordRecoveryConfiguration = passwordRecoveryConfiguration;
            _portAndHostConfiguration = portAndHostConfiguration;
        }

        public MailMessage BuildMailMessage(string emailBody, string email)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_passwordRecoveryConfiguration.Email);
            message.To.Add(new MailAddress(email));
            message.Subject = CommonConstants.MessageSubject;
            message.IsBodyHtml = true;
            message.Body = emailBody;
            return message;
        }

        public bool SendRecoveryEmail(string email, string emailBody)
        {
            try
            {
                MailMessage message = BuildMailMessage(emailBody, email);
                SmtpClient smtp = new SmtpClient();
                smtp.Port = Int32.Parse(_portAndHostConfiguration.SmtpPort);
                smtp.Host = _portAndHostConfiguration.SmtpHost;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_passwordRecoveryConfiguration.Email, _passwordRecoveryConfiguration.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
