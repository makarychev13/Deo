using System;
using System.Threading;
using System.Threading.Tasks;

using DomainServices.Orders.Commands.PullOrders;

using MediatR;

using Microsoft.Extensions.Hosting;

namespace Presentation.Orders.Ports.BackgroundServices
{
    public sealed class PullOrdersBackgroundService : BackgroundService
    {
        private readonly IMediator _mediator;

        public PullOrdersBackgroundService(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                await _mediator.Send(new PullOrdersCommand(), stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}