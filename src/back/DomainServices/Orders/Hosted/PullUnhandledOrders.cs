using System;
using System.Threading.Tasks;
using Common.HostedServices;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using Infrastructure.Orders.Repositories;
using Infrastructure.Orders.Rss.Reader;

namespace DomainServices.Orders.Hosted
{
    public sealed class PullUnhandledOrders : CommonHostedService
    {
        protected override TimeSpan Period => TimeSpan.FromMinutes(4);

        private readonly FreelanceBursesRepository _freelanceBursesRepository;
        private readonly OrdersRepository _ordersRepository;
        private readonly IOrdersReader _ordersReader;

        public PullUnhandledOrders(
            FreelanceBursesRepository freelanceBursesRepository,
            OrdersRepository ordersRepository, IOrdersReader ordersReader)
        {
            _freelanceBursesRepository = freelanceBursesRepository;
            _ordersRepository = ordersRepository;
            _ordersReader = ordersReader;
        }

        protected override async Task ExecuteAsync()
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