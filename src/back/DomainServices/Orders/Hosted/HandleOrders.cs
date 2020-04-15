using System;
using System.Linq;
using System.Threading.Tasks;
using Common.HostedServices;
using Common.Kafka.Producer;
using Domain.Orders;
using Infrastructure.Orders.Repositories;

namespace DomainServices.Orders.Hosted
{
    public class HandleOrders : BaseHostedService
    {
        protected override TimeSpan Period => TimeSpan.FromMinutes(2);

        private readonly OrdersRepository _ordersRepository;
        private readonly KafkaProducer<string, Order> _producer;

        public HandleOrders(OrdersRepository ordersRepository, KafkaProducer<string, Order> producer)
        {
            _ordersRepository = ordersRepository;
            _producer = producer;
        }

        protected override async Task ExecuteAsync()
        {
            Order[] orders = await _ordersRepository.GetUnhandledOrdersAsync();
            foreach (Order order in orders)
            {
                await _producer.ProduceAsync(order.Source.Name, order);
            }

            await _ordersRepository.HandleOrders(orders.Select(p => p.Body.Link));
        }
    }
}