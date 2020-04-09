using System;
using Domain.Notifications.Messages.ValueObjects;

namespace Domain.Notifications.Messages
{
    public sealed class TelegramMessage : Message
    {
        public readonly TelegramBody Body;
        
        public TelegramMessage(string to, TelegramBody body) : base(to)
        {
            Body = body;
        }
    }
}