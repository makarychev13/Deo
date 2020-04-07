using System;
using Domain.Notifications.Messages.ValueObjects;

namespace Domain.Notifications.Messages
{
    public sealed class TelegramMessage
    {
        public readonly string Id;
        public readonly TelegramBody Body;

        public TelegramMessage(string id, TelegramBody body)
        {
            Id = id;
            Body = body;
        }

        public static TelegramBody BodyFrom()
        {
            throw new NotImplementedException();
        }
    }
}