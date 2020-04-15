using System;
using Domain.Orders;

namespace Domain.Notifications.Messages.ValueObjects
{
    public sealed class EmailBody
    {
        public readonly string Subject;
        public readonly string HtmlBody;

        public EmailBody(string subject, string htmlBody)
        {
            Subject = subject;
            HtmlBody = htmlBody;
        }

        public static EmailBody CreateFrom(Order order)
        {
            throw new NotImplementedException();
        }
    }
}