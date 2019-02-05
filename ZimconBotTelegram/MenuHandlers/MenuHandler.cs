using System;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

using ZimconBotTelegram.Contracts;

namespace ZimconBotTelegram.MenuHandlers
{
    public class MenuHandler : ITelegramMenu
    {
        public (IReplyMarkup, long, string) DoReply(Telegram.Bot.Types.Message message)
        {
            ReplyKeyboardMarkup ReplyKeyboard = new[]
            {
                new[] {"/menu", "/location"},
                //new[] {"/status", "/location"},
            };
            return (ReplyKeyboard, message.Chat.Id, "Choose");
        }
    }
}
