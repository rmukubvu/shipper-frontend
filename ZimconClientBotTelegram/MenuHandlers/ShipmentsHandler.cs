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
            //var list = new List<string>();
            ////if (shipments.Count < 10) // to come up with way to handle many shipments
            ////{
            ////    foreach (var shipment in shipments)
            ////    {
            ////        //list.Add($"{shipment.dashboardStatus.currentStatus}\n{shipment.shipment.manifestReference}\n{shipment.dashboardStatus.label}");
            ////    }
            ////}
            //var coordinates = $"{shipments[0].shipment.destinationLatitude};{shipments[0].shipment.destinationLongitude}";
            ////var inlineKeyboard = InlineButtonHelper.CreateInReplyKeyboardMarkup(list, 2);
            //return (null, message.Chat.Id, coordinates);
        }
    }
}
