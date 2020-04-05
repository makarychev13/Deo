using System;
using System.Threading.Tasks;
using Common.HostedServices;
using Domain.Orders;
using Infrastructure.Orders.Repositories;

namespace DomainServices.Orders.Hosted
{
    public class HandleOrders : CommonHostedService
    {
        private readonly OrdersRepository _ordersRepository;

        public HandleOrders(OrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        protected override async Task ExecuteAsync()
        {
            Order[] orders = await _ordersRepository.GetUnhandledOrdersAsync();
            
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(5);
    }
}