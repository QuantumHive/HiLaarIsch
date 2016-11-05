namespace QuantumHive.Core
{
    public interface IMessageService
    {
        void SendMessage(Message message);
    }

    public class Message
    {
        public string Destination { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
