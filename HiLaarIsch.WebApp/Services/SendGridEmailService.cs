using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using QuantumHive.Core;

namespace HiLaarIsch.Services
{
    public class SendGridEmailService : IMessageService
    {
        private const string contentType = @"text/plain";
        private readonly string fromAddress;
        private readonly string apiKey;

        public SendGridEmailService(string fromAddress, string apiKey)
        {
            this.fromAddress = fromAddress;
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
            var from = new Email(this.fromAddress);
            var to = new Email(message.Destination);
            var content = new Content(contentType, message.Body);

            return new Mail(from, message.Subject, to, content);
        }
    }
}