using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Notifications;
using Domain.Users;
using Infrastructure.Notifications;
using Infrastructure.Notifications.Repositories;
using Infrastructure.Users.Repositories;
using MediatR;

namespace DomainServices.Notifications.Commands.CreateNotifications
{
    public sealed class CreateNotificationsCommandHandler : INotificationHandler<CreateNotificationsCommand>
    {
        private readonly UsersRepository _usersRepository;
        private readonly NotificationsFabric _notificationsFabric;
        private readonly OutboxNotificationsRepository _outboxNotificationsRepository;

        public CreateNotificationsCommandHandler(UsersRepository usersRepository, NotificationsFabric notificationsFabric, OutboxNotificationsRepository outboxNotificationsRepository)
        {
            _usersRepository = usersRepository;
            _notificationsFabric = notificationsFabric;
            _outboxNotificationsRepository = outboxNotificationsRepository;
        }

        public async Task Handle(CreateNotificationsCommand request, CancellationToken cancellationToken)
        {
            User[] users = await _usersRepository.GetForNotifications(request.Order);
            if (!users.Any())
            {
                return;
            }
            
            Dictionary<Subscriptions, List<Message>> messages = _notificationsFabric.Create(users, request.Order);
            await _outboxNotificationsRepository.SaveToPush(messages, request.Order.Body.Link.ToString());
        }
    }
}