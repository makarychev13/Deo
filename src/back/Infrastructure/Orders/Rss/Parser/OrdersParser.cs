using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Orders.Rss.Parser
{
    public class OrdersParser : IOrdersParser
    {
        public List<Order> GetFrom(XDocument xml)
        {
            return xml.Elements("rss").Elements().Elements("item")
                .Select(p => new Order(
                    p.Element("title").Value,
                    p.Element("description").Value,
                    new Uri(p.Element("link").Value),
                    DateTime.Parse(p.Element("pubDate").Value)))
                .ToList();
        }

        public XDocument ToXml(IEnumerable<Order> orders)
        {
            var channel = new XElement("channel");
            foreach (var order in orders)
            {
                var item = new XElement("item");       
                item.Add(new XElement("title", order.Title));
                item.Add(new XElement("link", order.Link));
                item.Add(new XElement("description", order.Description));
                item.Add(new XElement("pubDate", order.Publication.ToString()));
                channel.Add(item);
            }
            var root = new XElement("rss", new XAttribute("version", "2.0"));
            root.Add(channel);

            return new XDocument(root);
        }
    }
}