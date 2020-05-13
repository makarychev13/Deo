using System.Collections.Generic;
using Domain.Notifications;
using Domain.Orders;
using Domain.Users;

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
                    messages[Subscriptions.Email].Add(new Message(user.Contact.Email, order.Body.Title, order.Body.Description));
                }

                if (user.Subscriptions == Subscriptions.Telegram)
                {
                    messages[Subscriptions.Telegram].Add(new Message(user.Contact.TelegramId, order.Body.Title, order.Body.Description));
                }
            }

            return messages;
        }
    }
}