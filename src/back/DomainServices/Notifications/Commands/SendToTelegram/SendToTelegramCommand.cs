using Domain.Notifications;
using MediatR;

namespace DomainServices.Notifications.Commands.SendToTelegram
{
    public sealed class SendToTelegramCommand : INotification
    {
        public readonly Message Message;

        public SendToTelegramCommand(Message message)
        {
            Message = message;
        }
    }
}