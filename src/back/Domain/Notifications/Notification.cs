using Domain.Notifications.Messages;

namespace Domain.Notifications
{
    public sealed class Notification
    {
        public readonly Subscriptions Transport;
        public readonly Message Message;

        public Notification(Subscriptions transport, Message message)
        {
            Transport = transport;
            Message = message;
        }
    }
}