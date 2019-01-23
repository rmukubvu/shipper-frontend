using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ZimconBotTelegram.Contracts
{
    public interface ITelegramMenu
    {
        (IReplyMarkup, long, string) DoReply(Telegram.Bot.Types.Message message);
    }
}
