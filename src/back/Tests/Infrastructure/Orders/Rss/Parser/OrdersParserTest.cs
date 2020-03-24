using System;
using System.Collections.Generic;
using System.Xml.Linq;

using Domain.Orders.ValueObjects;

using Infrastructure.Orders.Rss.Parser;

using Xunit;

namespace Tests.Infrastructure.Orders.Rss.Parser
{
    public class OrdersParserTest
    {
        public OrdersParserTest()
        {
            _parser = new OrdersParser();
        }

        private readonly IOrdersParser _parser;

        [Fact]
        public void GetFromTest()
        {
            List<OrderBody> orders = _parser.GetFrom(XDocument.Load("https://freelance.ru/rss/projects.xml"));

            Assert.NotEmpty(orders);
            Assert.NotNull(orders);
            Assert.DoesNotContain(
                orders,
                o => string.IsNullOrEmpty(o.Title) &&
                    string.IsNullOrEmpty(o.Description) &&
                    o.Link is null &&
                    o.Publication == default);
        }

        [Fact]
        public void ToXmlTest()
        {
            var orders = new[]
            {
                new OrderBody("title1", "description1", new Uri("https://ru.wikipedia.org/wiki"), DateTime.Now),
                new OrderBody("title2", "description2", new Uri("https://ru.wikipedia.org"), DateTime.Now.AddDays(1)),
                new OrderBody("title3", "description3", new Uri("https://ru.wikipedia.org"), DateTime.Now.AddDays(2))
            };
            XDocument xml = _parser.ToXml(orders);
            List<OrderBody> parsedOrders = _parser.GetFrom(xml);

            Assert.Equal(orders, parsedOrders);
        }
    }
}