namespace Domain.Notifications.Messages
{
    public abstract class Message
    {
        public readonly string To;

        protected Message(string to)
        {
            To = to;
        }
    }
}