using System;

using Common.Kafka.Producer;

using Confluent.Kafka;

using Domain.Notifications;

using Microsoft.Extensions.Options;

namespace Infrastructure.Notifications.KafkaProducers
{
    public sealed class NotificationKafkaProducer : KafkaProducer<string, Notification>
    {
        public NotificationKafkaProducer(IOptions<KafkaProducerConfig<string, Notification>> topicOptions, IProducer<string, Notification> producer) : base(topicOptions, producer)
        {
        }

        public override string GetTopic(string key, Notification value)
        {
            string topic;

            if (key.Equals(Subscriptions.Email.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                topic = "email";
            }
            else if (key.Equals(Subscriptions.Telegram.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                topic = "telegram";
            }
            else if (key.Equals(Subscriptions.Vk.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                topic = "vk";
            }
            else
            {
                throw new ApplicationException($"Не удалось сопоставить топик для сообщения типа ${key}");
            }

            return topic;
        }
    }
}