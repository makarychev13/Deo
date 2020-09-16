using System;
using System.Threading.Tasks;

using Common.HostedServices;

using DomainServices.Notifications.Commands.FlushNotifications;

using MediatR;

namespace Infrastructure.Notifications.BackgroundServices
{
    public sealed class FlushNotificationsBackgroundService : BaseHostedService
    {
        private readonly IMediator _mediator;

        public FlushNotificationsBackgroundService(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(1);

        protected override async Task ExecuteAsync()
        {
            await _mediator.Send(new FlushNotificationsCommand());
        }
    }
}