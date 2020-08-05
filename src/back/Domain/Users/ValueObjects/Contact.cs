namespace Domain.Users.ValueObjects
{
    public sealed class Contact
    {
        public readonly string Email;
        public readonly string TelegramId;

        public Contact(string telegramId, string email)
        {
            TelegramId = telegramId;
            Email = email;
        }
    }
}