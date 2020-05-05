using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Notifications.Messages;

namespace DomainServices.Notifications.Kafka
{
    public sealed class SendEmail : IKafkaHandler<string, EmailMessage>
    {
        private readonly SmtpClient _smtpClient;
        
        public SendEmail(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }
        
        public async Task HandleAsync(string key, EmailMessage message)
        {
            var eMailMessage = new MailMessage("makar.tula@gmail.com", message.To)
            {
                Subject = message.Body.Subject,
                Body = message.Body.HtmlBody,
                IsBodyHtml = true
            };
            
            using (eMailMessage) 
            {
                _smtpClient.Send(eMailMessage);
            }
        }
    }
}