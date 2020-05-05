using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Domain.Notifications;
using Domain.Orders;
using Domain.Users;
using Infrastructure.Notifications;
using Infrastructure.Notifications.Repositories;
using Infrastructure.Users.Repositories;

namespace DomainServices.Notifications.Kafka
{
    public sealed class CreateNotificationsFromOrder : IKafkaHandler<string, Order>
    {
        private readonly UsersRepository _usersRepository;
        private readonly NotificationsFabric _notificationsFabric;
        private readonly OutboxNotificationsRepository _outboxNotificationsRepository;

        public CreateNotificationsFromOrder(
            UsersRepository usersRepository,
            NotificationsFabric notificationsFabric,
            OutboxNotificationsRepository outboxNotificationsRepository)
        {
            _usersRepository = usersRepository;
            _notificationsFabric = notificationsFabric;
            _outboxNotificationsRepository = outboxNotificationsRepository;
        }
        
        public async Task HandleAsync(string key, Order message)
        {
            User[] users = await _usersRepository.GetForNotifications(message);
            if (!users.Any())
            {
                return;
            }
            
            Dictionary<Subscriptions, List<Message>> messages = _notificationsFabric.Create(users, message);
            await _outboxNotificationsRepository.SaveToPush(messages, message.Body.Link.ToString());
        }
    }
}