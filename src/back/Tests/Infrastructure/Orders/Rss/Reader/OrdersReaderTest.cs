using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Domain.Orders.ValueObjects;

using Infrastructure.Orders.Rss.Parser;
using Infrastructure.Orders.Rss.Reader;

using Xunit;

namespace Tests.Infrastructure.Orders.Rss.Reader
{
    public class OrdersReaderTest
    {
        public OrdersReaderTest()
        {
            _parser = new OrdersParser();
            _reader = new OrdersReader(
                _parser,
                new FreelanceBurse(new Uri("https://freelance.ru/rss/projects.xml"), "test"),
                fileName);
        }

        private const string fileName = "test.xml";

        private readonly IOrdersReader _reader;
        private readonly IOrdersParser _parser;

        [Fact]
        public async Task GetUnhandledAsyncTest()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            OrderBody[] ordersBeforeConfirm = _reader.GetHandled();
            OrderBody[] newOrders = await _reader.GetUnhandledAsync();
            _reader.Handle(newOrders);
            OrderBody[] ordersAfterConfirm = _reader.GetHandled();

            Assert.Empty(ordersBeforeConfirm);
            Assert.NotEmpty(newOrders);
            Assert.Equal(newOrders.Length, ordersAfterConfirm.Length);
            Assert.Equal(newOrders, ordersAfterConfirm);
        }

        [Fact]
        public async Task HandleTest()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            OrderBody[] unhandledOrders = await _reader.GetUnhandledAsync();
            _reader.Handle(unhandledOrders);

            var newOrders = new List<OrderBody>
            {
                new OrderBody(
                    "title1",
                    "description1",
                    new Uri("https://freelance.ru/rss/projects.xml"),
                    DateTime.Now),
                new OrderBody(
                    "title2",
                    "description2",
                    new Uri("https://freelance.ru/rss"),
                    DateTime.Now)
            };
            List<OrderBody> newIncomeOrders = newOrders.Concat(unhandledOrders).ToList();
            newIncomeOrders.RemoveRange(newIncomeOrders.Count - newOrders.Count - 1, newOrders.Count);

            _reader.Handle(newOrders);

            OrderBody[] handledOrders = _reader.GetHandled();

            Assert.Equal(newIncomeOrders.Count, handledOrders.Length);
            Assert.Equal(newIncomeOrders, handledOrders);
        }
    }
}