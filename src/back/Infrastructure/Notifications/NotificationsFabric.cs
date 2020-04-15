using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Domain.Notifications;
using Domain.Notifications.Messages;
using Domain.Notifications.Messages.ValueObjects;
using Domain.Orders;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Infrastructure.Notifications
{
    public sealed class NotificationsFabric
    {
        public Dictionary<Subscriptions, List<Message>> Create(User[] users, Order order)
        {
            var messages = new Dictionary<Subscriptions, List<Message>>()
            {
                {Subscriptions.Email, new List<Message>()},
                {Subscriptions.Telegram, new List<Message>()},
                {Subscriptions.Vk, new List<Message>()}
            };

            foreach (var user in users)
            {
                if (user.Subscriptions == Subscriptions.Email)
                {
                    messages[Subscriptions.Email].Add(new EmailMessage(user.Contact.Email, EmailBody.CreateFrom(order)));
                }

                if (user.Subscriptions == Subscriptions.Telegram)
                {
                    messages[Subscriptions.Telegram].Add(new TelegramMessage(user.Contact.TelegramId, TelegramBody.CreateFrom(order)));
                }
            }

            return messages;
        }
    }
}