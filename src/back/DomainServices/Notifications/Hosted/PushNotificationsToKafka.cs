using System;
using System.Threading.Tasks;
using Common.HostedServices;

namespace DomainServices.Notifications.Hosted
{
    public sealed class PushNotificationsToKafka : BaseHostedService
    {
        protected override Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(1);
    }
}