using System;
using System.Threading.Tasks;
using Common.HostedServices;
using DomainServices.Orders.Commands.PullOrders;
using MediatR;

namespace Presentation.Ports.BackgroundServices.Orders
{
    public sealed class PullOrdersBackgroundService : BaseHostedService
    {
        private readonly IMediator _mediator;

        public PullOrdersBackgroundService(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync()
        {
            await _mediator.Publish(new PullOrdersCommand());
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(5);
    }
}