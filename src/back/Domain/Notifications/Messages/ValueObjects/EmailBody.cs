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
            string subject = order.Body.Title;
            string htmlBody = $@"
            <b>{order.Body.Title}<b>
            <br>
            {order.Body.Description}
            <a href='{order.Body.Link}'>Подробнее</a>";
            
            return new EmailBody(subject, htmlBody);
        }
    }
}