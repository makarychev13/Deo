using Domain.Notifications.Messages.ValueObjects;
using Domain.Orders;

namespace Domain.Notifications.Messages
{
    public sealed class EmailMessage
    {
        public readonly string To;
        public readonly EmailBody EmailBody;

        public EmailMessage(string to, EmailBody emailBody)
        {
            To = to;
            EmailBody = emailBody;
        }

        public static EmailBody BodyFrom(Order order)
        {
            var subject = $"{order.Source.Name} - {order.Body.Title}";
            var htmlBody = $@"
                <h2>{order.Body.Title}</h2>
                <br>
                <p>{order.Body.Description}</p>
                <a href='{order.Body.Link} target='_blank'>Подробнее</a>";
            return new EmailBody(subject, htmlBody);
        }
    }
}