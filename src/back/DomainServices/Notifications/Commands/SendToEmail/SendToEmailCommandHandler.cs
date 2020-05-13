using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DomainServices.Notifications.Commands.SendToEmail
{
    internal sealed class SendToEmailCommandHandler : INotificationHandler<SendToEmailCommand>
    {
        private readonly SmtpClient _smtpClient;

        public SendToEmailCommandHandler(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task Handle(SendToEmailCommand request, CancellationToken cancellationToken)
        {
            var eMailMessage = new MailMessage("makar.tula@gmail.com", request.Message.To)
            {
                Subject = request.Message.Subject,
                Body = request.Message.Body,
                IsBodyHtml = true
            };
            
            using (eMailMessage) 
            {
                _smtpClient.Send(eMailMessage);
            }
        }
    }
}