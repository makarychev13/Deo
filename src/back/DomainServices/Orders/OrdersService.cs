using System;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

using Domain.Orders;
using Domain.Orders.ValueObjects;

using Infrastructure.Orders.Rss.Reader;

namespace DomainServices.Orders
{
    public sealed class OrdersService : IDisposable
    {
        private readonly IProducer<Null, Order> _producer;
        private readonly IOrdersReader _reader;

        public OrdersService(IOrdersReader reader, IProducer<Null, Order> producer)
        {
            _reader = reader;
            _producer = producer;
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task HandleOrdersAsync()
        {
            using (Mutex mutex = _reader.GetProccesLock())
            {
                mutex.WaitOne(TimeSpan.FromMinutes(1));
                OrderBody[] orders = await _reader.GetUnhandledAsync();

                foreach (OrderBody order in orders)
                {
                    await _producer.ProduceAsync("orders", new Message<Null, Order> { Value = null });
                }

                _reader.Handle(orders);
                mutex.ReleaseMutex();
            }
        }
    }
}