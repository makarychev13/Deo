using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Notifications.Messages;

namespace DomainServices.Notifications.Kafka
{
    public sealed class EmailMessageHandler : KafkaConsumer<string, EmailMessage>
    {
        private readonly SmtpClient _smtpClient;
        
        public EmailMessageHandler(ConsumerConfig options, SmtpClient smtpClient) : base(options)
        {
            _smtpClient = smtpClient;
        }

        protected override string Topic => "notifications";

        protected override async Task ConsumeAsync(string key, EmailMessage message)
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

        protected override bool NeedConsume(string key, EmailMessage message)
        {
            return key.Equals("\"Email\"", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}