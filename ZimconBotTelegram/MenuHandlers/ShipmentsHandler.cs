using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ZimconBotTelegram.Contracts;
using ZimconBotTelegram.Helper;

namespace ZimconBotTelegram.MenuHandlers
{
    public class ShipmentsHandler : ITelegramMenu
    {
        private Business business = new Business();

        public (IReplyMarkup, long, string) DoReply(Message message)
        {
            var shipments = business.GetShipmentOnVehicle(message.From.Id);
            var list = new List<string>();
            if (shipments.Count < 10) // to come up with way to handle many shipments
            {
                foreach (var shipment in shipments)
                {
                    var status = business.GetDashboardStatusByWaybill(shipment.wayBill);
                    list.Add($"{shipment.consignee}\n{shipment.manifestReference}\n{status.currentStatus}");
                }
            }
            var inlineKeyboard = InlineButtonHelper.CreateInReplyKeyboardMarkup(list, 2);
            return (inlineKeyboard, message.Chat.Id, "Change the Status");
        }
    }
}
