using System;
using Domain.Orders;

namespace Domain.Notifications.Messages.ValueObjects
{
    public sealed class TelegramBody
    {
        public readonly string MarkdownBody;

        public TelegramBody(string markdownBody)
        {
            MarkdownBody = markdownBody;
        }

        public static TelegramBody CreateFrom(Order order)
        {
            throw new NotImplementedException();
        }
    }
}