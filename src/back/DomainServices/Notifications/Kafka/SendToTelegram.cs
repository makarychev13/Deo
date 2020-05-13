using System.Threading.Tasks;
using Common.Kafka.Consumer;
using DomainServices.Notifications.Kafka.Contracts;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace DomainServices.Notifications.Kafka
{
    public sealed class SendToTelegram : IKafkaHandler<string, TelegramNotification>
    {
        private readonly ITelegramBotClient _bot;

        public SendToTelegram(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task HandleAsync(string key, TelegramNotification value)
        {
            try
            {
                await _bot.SendTextMessageAsync(value.Message.To, $"<b>{value.Message.Subject}</b>\n\n{value.Message.Body}", ParseMode.Html);
            }
            catch (ApiRequestException err) when (err.ErrorCode == 403)
            {
                
            }
        }
    }
}