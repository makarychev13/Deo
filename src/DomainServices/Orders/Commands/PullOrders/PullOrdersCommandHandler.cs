using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Domain.Orders;

using Infrastructure.Orders.Repositories.FreelanceBurse;
using Infrastructure.Orders.Rss;

using MediatR;

namespace DomainServices.Orders.Commands.PullOrders
{
    public sealed class PullOrdersCommandHandler : AsyncRequestHandler<PullOrdersCommand>
    {
        private readonly FreelanceBurseRepository _freelanceBurseRepository;
        private readonly OrdersReader _ordersReader;
        private readonly OrdersRepository _ordersRepository;

        public PullOrdersCommandHandler(FreelanceBurseRepository freelanceBurseRepository, OrdersReader ordersReader, OrdersRepository ordersRepository)
        {
            _freelanceBurseRepository = freelanceBurseRepository;
            _ordersReader = ordersReader;
            _ordersRepository = ordersRepository;
        }

        protected override async Task Handle(PullOrdersCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<FreelanceBurse> burses = await _freelanceBurseRepository.GetAll();

            foreach (FreelanceBurse burse in burses)
            {
                IEnumerable<Order> orders = _ordersReader.GetFrom(burse);
                await _ordersRepository.Merge(orders, burse.Id, cancellationToken);
            }
        }
    }
}