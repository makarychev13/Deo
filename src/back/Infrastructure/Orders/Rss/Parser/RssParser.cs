using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Orders.Rss.Parser
{
    public class RssParser : IRssParser
    {
        public async Task<Order[]> GetFromAsync(Uri link)
        {
            await Task.CompletedTask;
            var xml = XDocument.Load(link.ToString());
            return xml.Elements("rss").Elements().Elements("item")
                .Select(p => new Order(
                    p.Element("title").Value,
                    p.Element("description").Value,
                    new Uri(p.Element("link").Value),
                    DateTime.Parse(p.Element("pubDate").Value)))
                .ToArray();
        }
    }
}