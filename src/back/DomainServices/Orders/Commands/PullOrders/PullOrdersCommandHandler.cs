using System.Threading;
using System.Threading.Tasks;

using Domain.Orders;
using Domain.Orders.ValueObjects;

using Infrastructure.Orders.Repositories;
using Infrastructure.Orders.Rss.Reader;

using MediatR;

namespace DomainServices.Orders.Commands.PullOrders
{
    public sealed class PullOrdersCommandHandler : AsyncRequestHandler<PullOrdersCommand>
    {
        private readonly FreelanceBursesRepository _freelanceBursesRepository;
        private readonly IOrdersReader _ordersReader;
        private readonly OrdersRepository _ordersRepository;

        public PullOrdersCommandHandler(FreelanceBursesRepository freelanceBursesRepository, OrdersRepository ordersRepository, IOrdersReader ordersReader)
        {
            _freelanceBursesRepository = freelanceBursesRepository;
            _ordersRepository = ordersRepository;
            _ordersReader = ordersReader;
        }

        protected override async Task Handle(PullOrdersCommand request, CancellationToken cancellationToken)
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