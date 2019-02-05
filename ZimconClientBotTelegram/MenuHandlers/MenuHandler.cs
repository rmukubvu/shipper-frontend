using System;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

using ZimconClientBotTelegram.Contracts;

namespace ZimconClientBotTelegram.MenuHandlers
{
    public class MenuHandler : ITelegramMenu
    {
        public (IReplyMarkup, long, string) DoReply(Telegram.Bot.Types.Message message)
        {
            ReplyKeyboardMarkup ReplyKeyboard = new[]
            {
                new[] {"/menu", "/shipment"},               
            };
            return (ReplyKeyboard, message.Chat.Id, "Choose");
        }
    }
}
