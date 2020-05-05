using System;
using System.Linq;
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
        private readonly KafkaProducer<string, EmailMessage> _emailProducer;
        private readonly KafkaProducer<string, TelegramMessage> _telegramProducer;

        public PushNotificationsToKafka(OutboxNotificationsRepository notificationsRepository, KafkaProducer<string, EmailMessage> emailProducer, KafkaProducer<string, TelegramMessage> telegramProducer)
        {
            _notificationsRepository = notificationsRepository;
            _emailProducer = emailProducer;
            _telegramProducer = telegramProducer;
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
                Task send = notification.Transport switch
                {
                    Subscriptions.Email => _emailProducer.ProduceAsync(Subscriptions.Email.ToString(), (EmailMessage)notification.Message),
                    Subscriptions.Telegram => _telegramProducer.ProduceAsync(Subscriptions.Telegram.ToString(), (TelegramMessage)notification.Message),
                    _ => throw new ApplicationException($"Не удалось отправить в кафку сообщение типа {notification.Transport}")
                };
                
                await send;
            }

            await _notificationsRepository.Handle(notifications.Select(p => p.Id));
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(1);
    }
}