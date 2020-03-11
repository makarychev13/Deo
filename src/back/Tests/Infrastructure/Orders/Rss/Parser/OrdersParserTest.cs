using System;
using System.Linq;
using System.Xml.Linq;
using Infrastructure.Orders.Rss;
using Infrastructure.Orders.Rss.Parser;
using Xunit;

namespace Tests.Infrastructure.Orders.Rss.Parser
{
    public class OrdersParserTest
    {
        private readonly IOrdersParser _parser;

        public OrdersParserTest()
        {
            _parser = new OrdersParser();
        }

        [Fact]
        public void GetFromTest()
        {
            var orders = _parser.GetFrom(XDocument.Load("https://freelance.ru/rss/projects.xml"));
            
            Assert.NotEmpty(orders);
            Assert.NotNull(orders);
            Assert.DoesNotContain(orders,
                o => string.IsNullOrEmpty(o.Title) &&
                     string.IsNullOrEmpty(o.Description) &&
                     o.Link is null &&
                     o.Publication == default);
        }

        [Fact]
        public void ToXmlTest()
        {
            var orders = new Order[]
            {
                new Order("title1", "description1", new Uri("https://ru.wikipedia.org/wiki"), DateTime.Now),
                new Order("title2", "description2", new Uri("https://ru.wikipedia.org"), DateTime.Now.AddDays(1)),
                new Order("title3", "description3", new Uri("https://ru.wikipedia.org"), DateTime.Now.AddDays(2)) 
            };
            var xml = _parser.ToXml(orders);
            var parsedOrders = _parser.GetFrom(xml);
            
            Assert.Equal(orders, parsedOrders);
        }
    }
}