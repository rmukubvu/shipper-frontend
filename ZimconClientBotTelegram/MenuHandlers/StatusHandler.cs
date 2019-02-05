using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ZimconClientBotTelegram.Contracts;
using ZimconClientBotTelegram.Helper;

namespace ZimconClientBotTelegram.MenuHandlers
{
    public class StatusHandler : ITelegramMenu
    {
        private Business business = new Business();

        public (IReplyMarkup, long, string) DoReply(Message message)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            var collection = business.GetClearingStatuses();
            foreach (var item in collection)
            {
                data.Add(item.status, item.statusId.ToString());
            }
            var inlineKeyboard = InlineButtonHelper.CreateInlineKeyboardButton(data, 2);
            return (inlineKeyboard, message.Chat.Id, "Choose");
        }
    }
}
