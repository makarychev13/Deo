using System;
using Domain.Orders.ValueObjects;

namespace Domain.Orders
{
    public sealed class Order
    {
        public readonly string Title;
        public readonly string Description;
        public readonly Uri Link;
        public readonly DateTime Publication;

        public readonly FreelanceBurse Burse;

        public Order(string title, string description, Uri link, DateTime publication, FreelanceBurse burse)
        {
            Title = title;
            Description = description;
            Link = link;
            Publication = publication;
            Burse = burse;
        }
    }
}