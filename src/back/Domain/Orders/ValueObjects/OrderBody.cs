using System;

namespace Domain.Orders.ValueObjects
{
    public sealed class OrderBody
    {
        public readonly string Title;
        public readonly string Description;
        public readonly Uri Link;
        public readonly DateTime Publication;

        public OrderBody(string title, string description, Uri link, DateTime publication)
        {
            Title = title;
            Description = description;
            Link = link;
            Publication = publication;
        }

        public override int GetHashCode()
        {
            return Link.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return obj?.GetHashCode() == GetHashCode();
        }
    }
}