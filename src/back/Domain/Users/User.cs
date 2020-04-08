using Domain.Notifications;
using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public sealed class User
    {
        public readonly bool Active;
        public readonly Contact Contact;
        public readonly Subscriptions Subscriptions;
        public readonly string[] WhiteKeywords;
        public readonly string[] BlackKeywords;

        public User(bool active, Contact contact, Subscriptions subscriptions, string[] whiteKeywords, string[] blackKeywords)
        {
            Active = active;
            Contact = contact;
            Subscriptions = subscriptions;
            WhiteKeywords = whiteKeywords;
            BlackKeywords = blackKeywords;
        }
    }
}