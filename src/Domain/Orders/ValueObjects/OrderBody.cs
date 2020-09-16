using System;

namespace Domain.Orders.ValueObjects
{
    public sealed class OrderBody
    {
        public readonly string Description;
        public readonly Uri Link;
        public readonly DateTime Publication;
        public readonly string Title;

        public OrderBody(string title, string description, Uri link, DateTime publication)
        {
            Title = title;
            Description = description;
            Link = link;
            Publication = publication;
        }
    }
}