using System.Threading.Tasks;
using QuantumHive.Core;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HiLaarIsch.Services
{
    public class SendGridEmailService : IMessageService
    {
        private readonly IApplicationDeployment environment;
        private readonly HiLaarischSettings.EmailAddress emailAddresses;
        private readonly string apiKey;

        public SendGridEmailService(IApplicationDeployment environment, HiLaarischSettings.EmailAddress emails, string apiKey)
        {
            this.environment = environment;
            this.emailAddresses = emails;
            this.apiKey = apiKey;
        }

        public void SendMessage(Message message)
        {
            this.SendMailAsync(message).Wait();
        }

        private async Task SendMailAsync(Message message)
        {
            var mail = this.PrepareMail(message);
            var sendgrid = new SendGridClient(apiKey);
            var response = await sendgrid.SendEmailAsync(mail);
        }

        private SendGridMessage PrepareMail(Message message)
        {
            var from = new EmailAddress(this.emailAddresses.From);
            var destination = this.environment.Phase == ApplicationPhase.Test ? this.emailAddresses.Test : message.Destination;
            var to = new EmailAddress(destination);

            return MailHelper.CreateSingleEmail(from, to, message.Subject, plainTextContent: message.Body, htmlContent: message.Body);
        }
    }
}