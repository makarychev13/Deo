using System.Net.Mail;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Domain.Notifications;

namespace DomainServices.Notifications.Kafka
{
    public sealed class SendEmail : IKafkaHandler<string, Notification>
    {
        private readonly SmtpClient _smtpClient;
        
        public SendEmail(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }
        
        public async Task HandleAsync(string key, Notification message)
        {
            var eMailMessage = new MailMessage("makar.tula@gmail.com", message.Message.To)
            {
                Subject = message.Message.Subject,
                Body = message.Message.Body,
                IsBodyHtml = true
            };
            
            using (eMailMessage) 
            {
                _smtpClient.Send(eMailMessage);
            }
        }
    }
}