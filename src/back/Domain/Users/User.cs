using Domain.Keywords;
using Domain.Notifications;
using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public sealed class User
    {
        public readonly int Id;
        public readonly bool Active;
        public readonly Contact Contact;
        public readonly Subscriptions Subscriptions;
        public readonly WhiteKeyword[] WhiteKeywords;
        public readonly BlackKeyword[] BlackKeywords;

        public User(int id, bool active, Contact contact, Subscriptions subscriptions, WhiteKeyword[] whiteKeywords, BlackKeyword[] blackKeywords)
        {
            Id = id;
            Active = active;
            Contact = contact;
            Subscriptions = subscriptions;
            WhiteKeywords = whiteKeywords;
            BlackKeywords = blackKeywords;
        }
    }
}