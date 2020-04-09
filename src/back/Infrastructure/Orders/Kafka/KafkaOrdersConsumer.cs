using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Notifications;
using Domain.Notifications.Messages;
using Domain.Orders;
using Domain.Users;
using Domain.Users.ValueObjects;
using Infrastructure.Notifications;
using Infrastructure.Users.Repositories;

namespace Infrastructure.Orders.Kafka
{
    public sealed class KafkaOrdersConsumer : KafkaConsumer<string, Order>
    {
        private readonly UsersRepository _usersRepository;
        private readonly NotificationsFabric _notificationsFabric;
        
        public KafkaOrdersConsumer(ConsumerConfig options, UsersRepository usersRepository, NotificationsFabric notificationsFabric) : base(options)
        {
            _usersRepository = usersRepository;
            _notificationsFabric = notificationsFabric;
        }

        protected override string Topic => "orders";
        
        protected override async Task ConsumeAsync(string key, Order message)
        {
            Dictionary<Subscriptions, Contact[]> users = await _usersRepository.GetForNotifications(message);
            var messages = new Dictionary<Subscriptions, Message[]>();
            
            Subscriptions[] subs = users.Keys.ToArray();
            foreach (var sub in subs)
            {
                var contacts = users[sub];
                var notif = _notificationsFabric.Greate(sub, message, contacts);
                messages[sub] = notif;
            }

            foreach (var msg in messages)
            {
                
            }
        }

        protected override bool NeedConsume(string key, Order message)
        {
            return true;
        }
    }
}