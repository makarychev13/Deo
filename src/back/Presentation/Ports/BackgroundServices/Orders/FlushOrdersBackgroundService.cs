﻿using System;
using System.Threading.Tasks;

using Common.HostedServices;

using DomainServices.Orders.Commands.FlushOrders;

using MediatR;

namespace Presentation.Ports.BackgroundServices.Orders
{
    public sealed class FlushOrdersBackgroundService : BaseHostedService
    {
        private readonly IMediator _mediator;

        public FlushOrdersBackgroundService(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(2);

        protected override async Task ExecuteAsync()
        {
            await _mediator.Send(new FlushOrdersCommand());
        }
    }
}