using System.Threading.Tasks;

using Common.Kafka.Consumer;

using Domain.Orders;

using DomainServices.Notifications.Commands.CreateNotifications;

using MediatR;

namespace Presentation.Ports.Kafka.Orders
{
    public sealed class OrdersHandler : IKafkaHandler<string, Order>
    {
        private readonly IMediator _mediator;

        public OrdersHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(string key, Order value)
        {
            await _mediator.Send(new CreateNotificationsCommand(value));
        }
    }
}