using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace DomainServices.Notifications.Commands.SendToTelegram
{
    public sealed class SendToTelegramCommandHandler : INotificationHandler<SendToTelegramCommand>
    {
        private readonly ITelegramBotClient _bot;

        public SendToTelegramCommandHandler(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task Handle(SendToTelegramCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _bot.SendTextMessageAsync(request.Message.To, $"<b>{request.Message.Subject}</b>\n\n{request.Message.Body}", ParseMode.Html, cancellationToken: cancellationToken);
            }
            catch (ApiRequestException err) when (err.ErrorCode == 403)
            {
                
            }
        }
    }
}