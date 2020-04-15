using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Notifications;
using Domain.Notifications.Messages;

namespace Infrastructure.Notifications.Repositories
{
    public class OutboxNotificationsRepository
    {
        public async Task SaveToFlush(IDictionary<Subscriptions, List<Message>> events)
        {
            throw new NotImplementedException();
        } 
    }
}