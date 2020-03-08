using System;
using Domain.Orders.ValueObjects;

namespace Domain.Orders
{
    public class Order
    {
        public readonly string Title;
        public readonly string Description;
        public readonly Uri Link;
        public readonly DateTime PublicationDate;

        public readonly FreelanceBurse Burse;

        public Order(string title, string description, Uri link, DateTime publicationDate, FreelanceBurse burse)
        {
            Title = title;
            Description = description;
            Link = link;
            PublicationDate = publicationDate;
            Burse = burse;
        }
    }
}