using System.Text.RegularExpressions;
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
            string markdownBody = $@"
                <b>{order.Body.Title}</b>
                <br>
                {Regex.Replace(order.Body.Description, "<.*?>", string.Empty)}";
            
            return new TelegramBody(markdownBody);
        }
    }
}