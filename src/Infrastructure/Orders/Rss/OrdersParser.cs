using System;
using System.Linq;
using System.Xml.Linq;

using Domain.Orders;
using Domain.Orders.ValueObjects;

namespace Infrastructure.Orders.Rss
{
    public sealed class OrdersParser
    {
        public Order[] GetFrom(FreelanceBurse burse)
        {
            return XDocument.Load(burse.Link.ToString())
                .Elements("rss")
                .Elements()
                .Elements("item")
                .Select(
                    p => new Order(
                        new OrderBody(
                            p.Element("title").Value,
                            p.Element("description").Value,
                            new Uri(p.Element("link").Value),
                            DateTime.Parse(p.Element("pubDate").Value)),
                        burse))
                .ToArray();
        }
    }
}