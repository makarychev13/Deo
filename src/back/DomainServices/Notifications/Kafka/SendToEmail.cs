using System.Net.Mail;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using DomainServices.Notifications.Kafka.Contracts;

namespace DomainServices.Notifications.Kafka
{
    public sealed class SendToEmail : IKafkaHandler<string, EmailNotification>
    {
        private readonly SmtpClient _smtpClient;
        
        public SendToEmail(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }
        
        public async Task HandleAsync(string key, EmailNotification message)
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