using System;

namespace Infrastructure.Orders.Rss
{
    public class Order
    {
        public readonly string Title;
        public readonly string Description;
        public readonly Uri Link;
        public readonly DateTime Publication;

        public Order(string title, string description, Uri link, DateTime publication)
        {
            Title = title;
            Description = description;
            Link = link;
            Publication = publication;
        }
        
        public override int GetHashCode()
            => Link.GetHashCode();

        public override bool Equals(object? obj)
            => obj?.GetHashCode() == GetHashCode();
    }
}