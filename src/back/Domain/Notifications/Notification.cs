using Domain.Notifications.Messages;

namespace Domain.Notifications
{
    public sealed class Notification
    {
        public readonly int Id; 
        public readonly Subscriptions Transport;
        public readonly Message Message;

        public Notification(int id, Subscriptions transport, Message message)
        {
            Id = id;
            Transport = transport;
            Message = message;
        }
    }
}