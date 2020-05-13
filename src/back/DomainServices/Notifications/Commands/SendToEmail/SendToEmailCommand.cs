using Domain.Notifications;
using MediatR;

namespace DomainServices.Notifications.Commands.SendToEmail
{
    public sealed class SendToEmailCommand : INotification
    {
        public readonly Message Message;

        public SendToEmailCommand(Message message)
        {
            Message = message;
        }
    }
}