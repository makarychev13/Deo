using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Kafka.Producer;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using Infrastructure.Orders.Rss.Reader;
using Microsoft.Extensions.Hosting;

namespace DomainServices.Orders
{
    public sealed class OrdersService : IHostedService, IDisposable
    {
        private readonly KafkaProducer<string, Order> _producer;
        private readonly IOrdersReader _reader;
        
        private Timer _timer;

        public OrdersService(IOrdersReader reader, KafkaProducer<string, Order> producer)
        {
            _reader = reader;
            _producer = producer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async state => await HandleOrdersAsync(), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        
        public void Dispose()
        {
            _timer.Dispose();
            _producer.Dispose();
        }
        
        private async Task HandleOrdersAsync()
        {
            OrderBody[] orders = await _reader.GetUnhandledAsync();

            foreach (OrderBody body in orders)
            {
                var order = new Order(body, _reader.Burse);
                await _producer.ProduceAsync(order.Source.Name, order);
            }

            _reader.Handle(orders);
        }
    }
}