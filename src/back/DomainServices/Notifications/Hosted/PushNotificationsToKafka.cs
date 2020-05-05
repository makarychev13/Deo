using System;
using System.Linq;
using System.Threading.Tasks;
using Common.HostedServices;
using Common.Kafka.Producer;
using Domain.Notifications;
using Infrastructure.Notifications.Repositories;

namespace DomainServices.Notifications.Hosted
{
    public sealed class PushNotificationsToKafka : BaseHostedService
    {
        private readonly OutboxNotificationsRepository _notificationsRepository;
        private readonly KafkaProducer<string, Notification> _notificationProducer;

        public PushNotificationsToKafka(OutboxNotificationsRepository notificationsRepository, KafkaProducer<string, Notification> notificationProducer)
        {
            _notificationsRepository = notificationsRepository;
            _notificationProducer = notificationProducer;
        }

        protected override async Task ExecuteAsync()
        {
            Notification[] notifications = await _notificationsRepository.GetUnhandled();
            if (!notifications.Any())
            {
                return;
            }
            
            foreach (Notification notification in notifications)
            {
                await _notificationProducer.ProduceAsync(notification.Transport.ToString(), notification);
            }

            await _notificationsRepository.Handle(notifications.Select(p => p.Id));
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(1);
    }
}