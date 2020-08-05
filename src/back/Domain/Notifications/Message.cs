namespace Domain.Notifications
{
    public sealed class Message
    {
        public readonly string Body;
        public readonly string Subject;
        public readonly string To;

        public Message(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}