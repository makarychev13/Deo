using System;

namespace Domain.Orders.ValueObjects
{
    public sealed class FreelanceBurse
    {
        public readonly int Id;
        public readonly Uri Link;
        public readonly string Name;

        public FreelanceBurse(int id, Uri link, string name)
        {
            Id = id;
            Link = link;
            Name = name;
        }
    }
}