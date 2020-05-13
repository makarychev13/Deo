using Domain.Notifications;

namespace DomainServices.Notifications.Kafka.Contracts
{
    public sealed class TelegramNotification : Notification
    {
        public TelegramNotification(int id, Subscriptions transport, Message message) : base(id, transport, message)
        {
        }
    }
}