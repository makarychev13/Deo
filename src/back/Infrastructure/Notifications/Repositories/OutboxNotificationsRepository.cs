using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Notifications;
using Domain.Notifications.Messages;

namespace Infrastructure.Notifications.Repositories
{
    public sealed class OutboxNotificationsRepository
    {
        public async Task SaveToPush(IDictionary<Subscriptions, List<Message>> events, string idempotencyKey)
        {
            throw new NotImplementedException();
        } 
    }
}