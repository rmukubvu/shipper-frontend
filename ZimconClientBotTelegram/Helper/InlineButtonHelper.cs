using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ZimconClientBotTelegram.Helper
{
    public static class InlineButtonHelper
    {
        public static IReplyMarkup CreateInlineKeyboardButton(Dictionary<string, string> buttonList, int columns)
        {
            var rows = (int)Math.Ceiling((double)buttonList.Count / (double)columns);
            var buttons = new InlineKeyboardButton[rows][];
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i] = buttonList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => InlineKeyboardButton.WithCallbackData(direction.Key, direction.Value))
                    .ToArray();
            }
            return new InlineKeyboardMarkup(buttons);
        }

        public static ReplyKeyboardMarkup CreateInReplyKeyboardMarkup(List<string> buttonList, int columns)
        {
            var rows = (int)Math.Ceiling((double)buttonList.Count / (double)columns);
            var buttons = new String[rows][];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = buttonList
                    .Skip(i * columns)
                    .Take(columns)
                    .Select(direction => direction)
                    .ToArray();
            }
            return buttons.ToArray();
        }
    }
}
