﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Domain.Orders.ValueObjects;

using Infrastructure.Orders.Rss.Parser;

namespace Infrastructure.Orders.Rss.Reader
{
    public class OrdersReader : IOrdersReader
    {
        public FreelanceBurse Burse { get; }
        private readonly string _fileName;
        private readonly IOrdersParser _parser;

        public OrdersReader(IOrdersParser parser, FreelanceBurse burse, string fileName)
        {
            _parser = parser;
            Burse = burse;
            _fileName = fileName;
        }

        public async Task<OrderBody[]> GetUnhandledAsync()
        {
            await Task.CompletedTask;
            XDocument xml = XDocument.Load(Burse.Link.ToString());
            List<OrderBody> orders = _parser.GetFrom(xml);
            if (File.Exists(_fileName))
            {
                List<OrderBody> oldOrders = _parser.GetFrom(XDocument.Load(_fileName));
                orders = orders.Except(oldOrders.AsEnumerable()).ToList();
            }

            return orders.ToArray();
        }

        public void Handle(IEnumerable<OrderBody> orders)
        {
            var oldOrders = new List<OrderBody>();

            if (File.Exists(_fileName))
            {
                oldOrders = _parser.GetFrom(XDocument.Load(_fileName));
                oldOrders.RemoveRange(oldOrders.Count - orders.Count() - 1, orders.Count());
            }

            XDocument xml = _parser.ToXml(orders.Concat(oldOrders));
            File.WriteAllText(_fileName, xml.ToString());
        }

        public OrderBody[] GetHandled()
        {
            if (!File.Exists(_fileName))
            {
                return Array.Empty<OrderBody>();
            }

            return _parser.GetFrom(XDocument.Load(_fileName)).ToArray();
        }
    }
}