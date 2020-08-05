using Domain.Notifications;

using MediatR;

namespace DomainServices.Notifications.Commands.SendToEmail
{
    public sealed class SendToEmailCommand : IRequest
    {
        public readonly Message Message;

        public SendToEmailCommand(Message message)
        {
            Message = message;
        }
    }
}