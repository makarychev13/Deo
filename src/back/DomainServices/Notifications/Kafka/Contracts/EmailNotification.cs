using Domain.Notifications;

namespace DomainServices.Notifications.Kafka.Contracts
{
    public sealed class EmailNotification : Notification
    {
        public EmailNotification(int id, Subscriptions transport, Message message) : base(id, transport, message)
        {
        }
    }
}