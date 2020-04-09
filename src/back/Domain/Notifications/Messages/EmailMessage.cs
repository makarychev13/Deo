using Domain.Notifications.Messages.ValueObjects;
using Domain.Orders;

namespace Domain.Notifications.Messages
{
    public sealed class EmailMessage : Message
    {
        public readonly EmailBody Body;
        
        public EmailMessage(string to, EmailBody body) : base(to)
        {
            Body = body;
        }
    }
}