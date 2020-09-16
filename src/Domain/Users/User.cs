using Domain.Notifications;
using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public sealed class User
    {
        public readonly bool Active;
        public readonly Contact Contact;
        public readonly Subscriptions Subscriptions;

        public User(bool active, Contact contact, Subscriptions subscriptions)
        {
            Active = active;
            Contact = contact;
            Subscriptions = subscriptions;
        }
    }
}