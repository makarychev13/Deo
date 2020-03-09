using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Domain.Orders;
using Infrastructure.Orders.Rss.Reader;

namespace DomainServices.Orders
{
    public sealed class OrdersService : IDisposable
    {
        private readonly IOrdersReader _reader;
        private readonly IProducer<Null, Order> _producer;

        public OrdersService(IOrdersReader reader, IProducer<Null, Order> producer)
        {
            _reader = reader;
            _producer = producer;
        }

        public async Task HandleOrdersAsync()
        {
            using (var mutex = _reader.GetProccesLock())
            {
                mutex.WaitOne(TimeSpan.FromMinutes(1));
                var orders = await _reader.GetNewAsync();
                foreach (var order in orders)
                {
                    await _producer.ProduceAsync("orders", new Message<Null, Order>() { Value = null });
                }

                _reader.UpdateOld(orders);
                mutex.ReleaseMutex();
            }
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}