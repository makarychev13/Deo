using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Common.Events;

using Domain.Orders;

using Infrastructure.Orders.Repositories;

using MediatR;

namespace DomainServices.Orders.Commands.FlushOrders
{
    public sealed class FlushOrdersCommandHandler : AsyncRequestHandler<FlushOrdersCommand>
    {
        private readonly IEventBus<string, Order> _eventBus;
        private readonly OrdersRepository _ordersRepository;

        public FlushOrdersCommandHandler(OrdersRepository ordersRepository, IEventBus<string, Order> eventBus)
        {
            _ordersRepository = ordersRepository;
            _eventBus = eventBus;
        }

        protected override async Task Handle(FlushOrdersCommand request, CancellationToken cancellationToken)
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