using System.Threading;
using System.Threading.Tasks;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using Infrastructure.Orders.Repositories;
using Infrastructure.Orders.Rss.Reader;
using MediatR;

namespace DomainServices.Orders.Commands.PullOrders
{
    public sealed class PullOrdersCommandHandler : INotificationHandler<PullOrdersCommand>
    {
        private readonly FreelanceBursesRepository _freelanceBursesRepository;
        private readonly OrdersRepository _ordersRepository;
        private readonly IOrdersReader _ordersReader;

        public PullOrdersCommandHandler(FreelanceBursesRepository freelanceBursesRepository, OrdersRepository ordersRepository, IOrdersReader ordersReader)
        {
            _freelanceBursesRepository = freelanceBursesRepository;
            _ordersRepository = ordersRepository;
            _ordersReader = ordersReader;
        }

        public async Task Handle(PullOrdersCommand request, CancellationToken cancellationToken)
        {
            FreelanceBurse[] burses = await _freelanceBursesRepository.GetAll();
            foreach (var burse in burses)
            {
                Order[] orders = await _ordersReader.GetFrom(burse);
                await _ordersRepository.MergePulledOrdersAsync(orders, burse.Id);
            }
        }
    }
}