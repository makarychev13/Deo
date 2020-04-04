using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using Infrastructure.Orders.Rss.Parser;

namespace Infrastructure.Orders.Rss.Reader
{
    public class OrdersReader : IOrdersReader
    {
        private readonly IOrdersParser _parser;

        public OrdersReader(IOrdersParser parser)
        {
            _parser = parser;
        }


        public async Task<Order[]> GetFrom(FreelanceBurse burse)
        {
            await Task.CompletedTask;
            XDocument xml = XDocument.Load(burse.Link.ToString());
            OrderBody[] bodies = _parser.GetFrom(xml);
            return bodies.Select(p => new Order(p, burse)).ToArray();
        }
    }
}