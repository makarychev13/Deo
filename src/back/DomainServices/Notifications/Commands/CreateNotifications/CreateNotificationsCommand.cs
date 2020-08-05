using Domain.Orders;

using MediatR;

namespace DomainServices.Notifications.Commands.CreateNotifications
{
    public sealed class CreateNotificationsCommand : IRequest
    {
        public readonly Order Order;

        public CreateNotificationsCommand(Order order)
        {
            Order = order;
        }
    }
}