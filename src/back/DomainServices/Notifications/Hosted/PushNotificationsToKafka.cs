using System;
using System.Threading.Tasks;
using Common.HostedServices;
using Common.Kafka.Producer;
using Domain.Notifications;
using Domain.Notifications.Messages;
using Infrastructure.Notifications.Repositories;

namespace DomainServices.Notifications.Hosted
{
    public sealed class PushNotificationsToKafka : BaseHostedService
    {
        private readonly OutboxNotificationsRepository _notificationsRepository;
        private readonly KafkaProducer<string, Message> _producer;

        public PushNotificationsToKafka(OutboxNotificationsRepository notificationsRepository, KafkaProducer<string, Message> producer)
        {
            _notificationsRepository = notificationsRepository;
            _producer = producer;
        }

        protected override async Task ExecuteAsync()
        {
            Notification[] notifications = await _notificationsRepository.GetUnhandled();
            foreach (Notification notification in notifications)
            {
                await _producer.ProduceAsync(notification.Transport.ToString(), notification.Message);
            }
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(1);
    }
}