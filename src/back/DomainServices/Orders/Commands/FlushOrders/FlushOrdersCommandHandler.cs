using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Events;
using Common.Kafka.Producer;
using Domain.Orders;
using Infrastructure.Orders.Repositories;
using MediatR;

namespace DomainServices.Orders.Commands.FlushOrders
{
    public sealed class FlushOrdersCommandHandler : INotificationHandler<FlushOrdersCommand>
    {
        private readonly OrdersRepository _ordersRepository;
        private readonly IEventBus<string, Order> _eventBus;

        public FlushOrdersCommandHandler(OrdersRepository ordersRepository, IEventBus<string, Order> eventBus)
        {
            _ordersRepository = ordersRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(FlushOrdersCommand request, CancellationToken cancellationToken)
        {
            Order[] orders = await _ordersRepository.GetUnhandledOrdersAsync();
            foreach (Order order in orders)
            {
                await _eventBus.PublishAsync(order.Source.Name, order);
            }

            await _ordersRepository.HandleOrders(orders.Select(p => p.Body.Link));
        }
    }
}