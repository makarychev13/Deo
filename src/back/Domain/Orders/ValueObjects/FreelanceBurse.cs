using System;

namespace Domain.Orders.ValueObjects
{
    public class FreelanceBurse
    {
        public readonly Uri Link;
        public readonly string Name;

        public FreelanceBurse(Uri link, string name)
        {
            Link = link;
            Name = name;
        }
    }
}