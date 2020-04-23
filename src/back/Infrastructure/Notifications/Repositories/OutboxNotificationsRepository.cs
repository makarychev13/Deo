using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Common.Repositories;
using Dapper;
using Domain.Notifications;
using Domain.Notifications.Messages;
using Migrations.Tables.OutboxNotifications;
using Newtonsoft.Json;

namespace Infrastructure.Notifications.Repositories
{
    public sealed class OutboxNotificationsRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public OutboxNotificationsRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task SaveToPush(IDictionary<Subscriptions, List<Message>> events, string orderLink)
        {
            string sql = $@"
                insert into ""OutboxNotifications""
                (""IdempotencyKey"", ""Data"", ""Transport"", ""Status"")
                values(@key, @data, @subscriptions, @status)";

            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                foreach (KeyValuePair<Subscriptions, List<Message>> @event in events)
                {
                    foreach (Message message in @event.Value)
                    {
                        await connection.ExecuteAsync(sql, new
                        {
                            key = $"{@event.Key}_{orderLink}_{message.To}".ToLower(),
                            data = JsonConvert.SerializeObject(message, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto }),
                            subscriptions = @event.Key,
                            status = OutboxNotificationsStatusEntity.New.ToString()
                        });   
                    }
                }
            }
        } 
    }
}