using System.Threading.Tasks;
using Common.Kafka.Consumer;
using DomainServices.Notifications.Commands.SendToTelegram;
using DomainServices.Notifications.Kafka.Contracts;
using MediatR;

namespace Presentation.Ports.Kafka.Notifications
{
    public sealed class TelegramNotificationHandler : IKafkaHandler<string, TelegramNotification>
    {
        private readonly IMediator _mediator;

        public TelegramNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(string key, TelegramNotification value)
        {
            await _mediator.Publish(new SendToTelegramCommand(value.Message));
        }
    }
}