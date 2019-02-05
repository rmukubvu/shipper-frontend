using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ZimconClientBotTelegram.Contracts;

namespace ZimconClientBotTelegram.MenuHandlers
{
    public class DefaultHandler : ITelegramMenu
    {
        public (IReplyMarkup, long, string) DoReply(Message message)
        {
            var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                KeyboardButton.WithRequestContact("My Phone Number")
            });
            return (RequestReplyKeyboard, message.Chat.Id,
                "To start you need to provide us with your mobile number by clicking on button below [My Phone Number]");

        }
    }
}
