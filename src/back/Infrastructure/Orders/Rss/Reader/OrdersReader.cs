using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Infrastructure.Orders.Rss.Parser;

namespace Infrastructure.Orders.Rss.Reader
{
    public class OrdersReader : IOrdersReader
    {
        private readonly IOrdersParser _parser;
        private readonly Uri _link;
        private readonly string _fileName;

        public OrdersReader(IOrdersParser parser, Uri link, string fileName)
        {
            _parser = parser;
            _link = link;
            _fileName = fileName;
        }

        public async Task<Order[]> GetUnhandledAsync()
        {
            await Task.CompletedTask;
            var xml = XDocument.Load(_link.ToString());
            var orders = _parser.GetFrom(xml);
            if (File.Exists(_fileName))
            {
                var oldOrders = _parser.GetFrom(XDocument.Load(_fileName));
                orders = orders.Except(oldOrders.AsEnumerable()).ToList();
            }

            return orders.ToArray();
        }

        public void Handle(IEnumerable<Order> orders)
        {
            var oldOrders = new List<Order>();
            if (File.Exists(_fileName))
            {
                oldOrders = _parser.GetFrom(XDocument.Load(_fileName));
                oldOrders.RemoveRange(oldOrders.Count - orders.Count() - 1, orders.Count());
            }
            var xml = _parser.ToXml(orders.Concat(oldOrders));
            File.WriteAllText(_fileName, xml.ToString());
        }

        public Mutex GetProccesLock()
        {
            return new Mutex(false, _fileName);
        }

        public Order[] GetHandled()
        {
            if (!File.Exists(_fileName))
            {
                return Array.Empty<Order>();
            }

            return _parser.GetFrom(XDocument.Load(_fileName)).ToArray();
        }
    }
}