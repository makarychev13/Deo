using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Domain.Notifications;
using Domain.Notifications.Messages;
using Domain.Notifications.Messages.ValueObjects;
using Domain.Orders;
using Domain.Users.ValueObjects;

namespace Infrastructure.Notifications
{
    public sealed class NotificationsFabric
    {
        public Message[] Greate(Subscriptions subscription, Order order, Contact[] contact)
        {
            throw new NotImplementedException();
        }
    }
}