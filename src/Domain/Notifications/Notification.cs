namespace Domain.Notifications
{
    public class Notification
    {
        public readonly int Id;
        public readonly Message Message;
        public readonly Subscriptions Transport;

        public Notification(int id, Subscriptions transport, Message message)
        {
            Transport = transport;
            Message = message;
            Id = id;
        }
    }
}