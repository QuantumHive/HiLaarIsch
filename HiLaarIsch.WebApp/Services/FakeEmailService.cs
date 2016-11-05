using System;
using System.IO;
using QuantumHive.Core;

namespace HiLaarIsch.Services
{
    public class FakeEmailService : IMessageService
    {
        private readonly string path;

        public FakeEmailService(string path)
        {
            this.path = path;
        }

        public void SendMessage(Message message)
        {
            var content =
$@"[{DateTime.Now.ToString("dd-MM-yyyy, mm:HH:ss")}]
Destination: {message.Destination}
    Subject: {message.Subject}
______________________
{message.Body}
";

            File.AppendAllText(this.path, content);
        }
    }
}