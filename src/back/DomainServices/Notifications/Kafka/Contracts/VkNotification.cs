using Domain.Notifications;

namespace DomainServices.Notifications.Kafka.Contracts
{
    public sealed class VkNotification : Notification
    {
        public VkNotification(int id, Subscriptions transport, Message message) : base(id, transport, message)
        {
        }
    }
}