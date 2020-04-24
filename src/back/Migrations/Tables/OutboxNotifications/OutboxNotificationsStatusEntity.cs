namespace Migrations.Tables.OutboxNotifications
{
    public enum OutboxNotificationsStatusEntity
    {
        New = 0,
        InProcess = 1,
        Finish = 2
    }
}