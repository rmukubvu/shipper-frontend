using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ZimconClientBotTelegram.Contracts;
using ZimconClientBotTelegram.Helper;

namespace ZimconClientBotTelegram.MenuHandlers
{
    public class ShipmentsHandler : ITelegramMenu
    {
        private Business business = new Business();

        public (IReplyMarkup, long, string) DoReply(Message message)
        {
            var collection = business.GetShipmentWithTelegramId(message.From.Id);
            Dictionary<string, string> data = new Dictionary<string, string>();            
            foreach (var item in collection)
            {
                data.Add(item.shipment.manifestReference, $"{item.shipment.destinationLatitude};{item.shipment.destinationLongitude};{item.dashboardStatus.currentStatus}");
            }
            var inlineKeyboard = InlineButtonHelper.CreateInlineKeyboardButton(data, 2);
            return (inlineKeyboard, message.Chat.Id, "Click to View Location");           
        }
    }
}
