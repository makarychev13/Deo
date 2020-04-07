namespace Domain.Users.ValueObjects
{
    public sealed class Contact
    {
        public readonly string TelegramId;
        public readonly string Email;

        public Contact(string telegramId, string email)
        {
            TelegramId = telegramId;
            Email = email;
        }
    }
}