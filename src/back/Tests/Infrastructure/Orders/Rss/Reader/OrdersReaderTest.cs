using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Infrastructure.Orders.Rss;
using Infrastructure.Orders.Rss.Parser;
using Infrastructure.Orders.Rss.Reader;
using Xunit;

namespace Tests.Infrastructure.Orders.Rss.Reader
{
    public class OrdersReaderTest
    {
        private const string fileName = "test.xml";
        private readonly IOrdersReader _reader;
        private readonly IOrdersParser _parser;

        public OrdersReaderTest()
        {
            _parser = new OrdersParser();
            _reader = new OrdersReader(
                _parser,
                new Uri("https://freelance.ru/rss/projects.xml"),
                fileName);
        }

        [Fact]
        public async Task HandleTest()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var unhandledOrders = await _reader.GetUnhandledAsync();
            _reader.Handle(unhandledOrders);

            var newOrders = new List<Order>()
            {
                new Order(
                    "title1",
                    "description1",
                    new Uri("https://freelance.ru/rss/projects.xml"),
                    DateTime.Now),
                new Order(
                    "title2",
                    "description2",
                    new Uri("https://freelance.ru/rss"),
                    DateTime.Now)
            };
            var newIncomeOrders = newOrders.Concat(unhandledOrders).ToList();
            newIncomeOrders.RemoveRange(newIncomeOrders.Count - newOrders.Count - 1, newOrders.Count);
            
            _reader.Handle(newOrders);

            var handledOrders = _reader.GetHandled();
            
            Assert.Equal(newIncomeOrders.Count, handledOrders.Length);
            Assert.Equal(newIncomeOrders, handledOrders); 
        }

        [Fact]
        public async Task GetUnhandledAsyncTest()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var ordersBeforeConfirm = _reader.GetHandled();
            var newOrders = await _reader.GetUnhandledAsync();
            _reader.Handle(newOrders);
            var ordersAfterConfirm = _reader.GetHandled();

            Assert.Empty(ordersBeforeConfirm);
            Assert.NotEmpty(newOrders);
            Assert.Equal(newOrders.Length, ordersAfterConfirm.Length);
            Assert.Equal(newOrders, ordersAfterConfirm);
        }
    }
}