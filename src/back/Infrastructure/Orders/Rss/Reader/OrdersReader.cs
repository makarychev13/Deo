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
        private readonly Uri _link;
        private readonly IOrdersParser _parser;
        private readonly Uri _fileName;

        public OrdersReader(Uri link, IOrdersParser parser, Uri fileName)
        {
            _link = link;
            _parser = parser;
            _fileName = fileName;
        }

        public async Task<Order[]> GetNewAsync()
        {
            await Task.CompletedTask;
            var xml = XDocument.Load(_link.ToString());
            var orders = _parser.GetFrom(xml);
            if (File.Exists(_fileName.Host))
            {
                var oldOrders = _parser.GetFrom(XDocument.Load(_fileName.Host));
                orders = orders.Except(oldOrders.AsEnumerable()).ToList();
            }

            return orders.ToArray();
        }

        public void UpdateOld(IEnumerable<Order> orders)
        {
            var xml = _parser.ToXml(orders);
            File.WriteAllText(_fileName.ToString(), xml.ToString());
        }

        public Mutex GetProccesLock()
        {
            return new Mutex(false, _fileName.ToString());
        }
    }
}