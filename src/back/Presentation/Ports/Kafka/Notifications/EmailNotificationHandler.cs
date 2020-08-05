using System.Threading.Tasks;

using Common.Kafka.Consumer;

using DomainServices.Notifications.Commands.SendToEmail;
using DomainServices.Notifications.Kafka.Contracts;

using MediatR;

namespace Presentation.Ports.Kafka.Notifications
{
    public sealed class EmailNotificationHandler : IKafkaHandler<string, EmailNotification>
    {
        private readonly IMediator _mediator;

        public EmailNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(string key, EmailNotification value)
        {
            await _mediator.Send(new SendToEmailCommand(value.Message));
        }
    }
}