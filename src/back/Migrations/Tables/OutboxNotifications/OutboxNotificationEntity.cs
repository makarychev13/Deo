using Domain.Notifications;

namespace Migrations.Tables.OutboxNotifications
{
    public class OutboxNotificationEntity
    {
        public int Id { get; set; }
        public string IdempotencyKey { get; set; }
        public string Data { get; set; }
        public Subscriptions Transport { get; set; }
        public OutboxNotificationsStatusEntity Status { get; set; }
    }
}