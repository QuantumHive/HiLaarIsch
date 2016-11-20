using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using QuantumHive.Core;

namespace HiLaarIsch.Services
{
    public class SendGridEmailService : IMessageService
    {
        private const string contentType = @"text/plain";

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
            var mail = this.PrepareMail(message);

            dynamic sendgrid = new SendGridAPIClient(apiKey);
            dynamic response = sendgrid.client.mail.send.post(requestBody: mail.Get());
        }

        private Mail PrepareMail(Message message)
        {
            var from = new Email(this.emailAddresses.From);
            var destination = this.environment.Phase == ApplicationPhase.Test ? this.emailAddresses.Test : message.Destination;
            var to = new Email(destination);
            var content = new Content(contentType, message.Body);

            return new Mail(from, message.Subject, to, content);
        }
    }
}