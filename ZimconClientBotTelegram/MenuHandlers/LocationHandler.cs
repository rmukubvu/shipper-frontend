using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ZimconClientBotTelegram.Contracts;

namespace ZimconClientBotTelegram.MenuHandlers
{
    public class LocationHandler : ITelegramMenu
    {
        public (IReplyMarkup, long, string) DoReply(Message message)
        {
            var RequestReplyKeyboardLoc = new ReplyKeyboardMarkup(new[]
            {
                KeyboardButton.WithRequestLocation("Send Current Location")
            });
            return (RequestReplyKeyboardLoc, message.Chat.Id, "Click button below to send location update");
        }
    }
}
