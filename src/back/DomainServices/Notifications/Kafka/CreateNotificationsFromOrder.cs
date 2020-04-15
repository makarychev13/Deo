using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Notifications;
using Domain.Notifications.Messages;
using Domain.Orders;
using Domain.Users;
using Infrastructure.Notifications;
using Infrastructure.Notifications.Repositories;
using Infrastructure.Users.Repositories;

namespace DomainServices.Notifications.Kafka
{
    public sealed class CreateNotificationsFromOrder : KafkaConsumer<string, Order>
    {
        private readonly UsersRepository _usersRepository;
        private readonly NotificationsFabric _notificationsFabric;
        private readonly OutboxNotificationsRepository _outboxNotificationsRepository;
        
        public CreateNotificationsFromOrder(ConsumerConfig options, UsersRepository usersRepository, NotificationsFabric notificationsFabric, OutboxNotificationsRepository outboxNotificationsRepository) : base(options)
        {
            _usersRepository = usersRepository;
            _notificationsFabric = notificationsFabric;
            _outboxNotificationsRepository = outboxNotificationsRepository;
        }

        protected override string Topic => "orders";
        
        protected override async Task ConsumeAsync(string key, Order message)
        {
            User[] users = await _usersRepository.GetForNotifications(message);
            Dictionary<Subscriptions, List<Message>> messages = _notificationsFabric.Create(users, message);
            await _outboxNotificationsRepository.SaveToPush(messages, message.Body.Link.ToString());
        }

        protected override bool NeedConsume(string key, Order message)
        {
            return true;
        }
    }
}