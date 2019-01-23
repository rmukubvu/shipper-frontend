﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using ZimconBotTelegram.Contracts;
using ZimconBotTelegram.MenuHandlers;

namespace ZimconBotTelegram
{
    public class MenuResponseFactory
    {
        static IDictionary<string,ITelegramMenu> dictionaryCache = new ConcurrentDictionary<string, ITelegramMenu>();

        static MenuResponseFactory()
        {
            dictionaryCache["/menu"] = new MenuHandler();
            dictionaryCache["/location"] = new LocationHandler();
            dictionaryCache["/status"] = new StatusHandler();
            dictionaryCache["/shipments"] = new ShipmentsHandler();
            dictionaryCache["/default"] = new DefaultHandler();
        }

        public (IReplyMarkup, long, string) DoAction(string menu, Telegram.Bot.Types.Message message)
        {
            if (dictionaryCache.ContainsKey(menu))
                return dictionaryCache[menu].DoReply(message);
            return dictionaryCache["/default"].DoReply(message); ;
        }
    }
}
