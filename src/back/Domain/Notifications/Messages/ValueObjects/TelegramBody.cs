namespace Domain.Notifications.Messages.ValueObjects
{
    public sealed class TelegramBody
    {
        public readonly string MarkdownBody;

        public TelegramBody(string markdownBody)
        {
            MarkdownBody = markdownBody;
        }
    }
}