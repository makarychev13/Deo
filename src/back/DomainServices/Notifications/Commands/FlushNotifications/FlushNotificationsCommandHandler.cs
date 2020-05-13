using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Events;
using Domain.Notifications;
using Infrastructure.Notifications.Repositories;
using MediatR;

namespace DomainServices.Notifications.Commands.FlushNotifications
{
    public sealed class FlushNotificationsCommandHandler : INotificationHandler<FlushNotificationsCommand>
    {
        private readonly OutboxNotificationsRepository _notificationsRepository;
        private readonly IEventBus<string, Notification> _eventBus;

        public FlushNotificationsCommandHandler(OutboxNotificationsRepository notificationsRepository, IEventBus<string, Notification> eventBus)
        {
            _notificationsRepository = notificationsRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(FlushNotificationsCommand request, CancellationToken cancellationToken)
        {
            Notification[] notifications = await _notificationsRepository.GetUnhandled();
            if (!notifications.Any())
            {
                return;
            }
            
            foreach (Notification notification in notifications)
            {
                await _eventBus.PublishAsync(notification.Transport.ToString(), notification);
            }

            await _notificationsRepository.Handle(notifications.Select(p => p.Id));
        }
    }
}