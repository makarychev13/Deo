using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Orders.Rss.Parser
{
    public class RssParser : IRssParser
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
    }
}