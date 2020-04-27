using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Dapper;
using Domain.Notifications;
using Domain.Notifications.Messages;
using Migrations.Tables.OutboxNotifications;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
                values(@key, @data, @subscriptions, @status)
                on conflict(""IdempotencyKey"") do nothing";

            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                foreach (KeyValuePair<Subscriptions, List<Message>> @event in events)
                {
                    foreach (Message message in @event.Value)
                    {
                        await connection.ExecuteAsync(sql, new
                        {
                            key = $"{@event.Key}_{orderLink}_{message.To}".ToLower(),
                            data = JsonConvert.SerializeObject(message, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }),
                            subscriptions = @event.Key.ToString(),
                            status = OutboxNotificationsStatusEntity.New.ToString()
                        });   
                    }
                }
            }
        }
        
        public async Task<Notification[]> GetUnhandled()
        {
            string sql = $@"
                with cte as(
                    update ""OutboxNotifications""
                    set 
                        ""Status"" = '{OutboxNotificationsStatusEntity.InProcess}',
                        ""LastModificationDate"" = now()
                    where 
                        ""Status"" = '{OutboxNotificationsStatusEntity.New}'
                        or (""Status"" = '{OutboxNotificationsStatusEntity.InProcess}' and date_part('minutes', now() - ""LastModificationDate""::timestamp) > 0)
                    returning ""Transport"", ""Data"", ""Id""
                )
                select ""Transport"", ""Data"", ""Id"" 
                from cte";

            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                var result = await connection.QueryAsync<OutboxNotificationEntity>(sql);

                return result
                    .Select(p =>
                    {
                        Message message = (Message)JsonConvert.DeserializeObject(p.Data, new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All});
                        return new Notification(p.Id, p.Transport, message);
                    }).ToArray();
            }
        }

        public async Task Handle(IEnumerable<int> ids)
        {
            string sql = $@"
                update ""OutboxNotifications""
                set 
                    ""Status"" = '{OutboxNotificationsStatusEntity.Finish}',
                    ""LastModificationDate"" = now()
                where ""Id"" = ANY(@ids)
            ";

            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                await connection.ExecuteAsync(sql, new { ids = ids.ToArray() });
            }
        }
    }
}