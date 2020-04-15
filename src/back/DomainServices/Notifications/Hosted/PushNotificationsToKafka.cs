using System;
using System.Threading.Tasks;
using Common.HostedServices;

namespace DomainServices.Notifications.Hosted
{
    public sealed class PushNotificationsToKafka : BaseHostedService
    {
        protected override Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        protected override TimeSpan Period => TimeSpan.FromMinutes(1);
    }
}