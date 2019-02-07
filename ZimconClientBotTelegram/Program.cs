using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace ZimconClientBotTelegram
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient(ConfigurationManager.AppSettings["telegram.token"]);
        private static readonly Business Business = new Business();
        private static readonly MenuResponseFactory MenuResponseFactory = new MenuResponseFactory();
        static void Main(string[] args)
        {
            var me = Bot.GetMeAsync().Result;
            Console.Title = me.Username;
            Business.CacheDevices();
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            //Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            Bot.OnReceiveError += BotOnReceiveError;
            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void DoDeviceLinking(Telegram.Bot.Types.Message message)
        {            
            //check device id is linked
            if (!Business.IsDeviceLinked(message.From.Id))
            {
                //register this device
                if (message.Contact == null)
                {                    //request phone number
                    var (keyboard, chatId, dialog) = MenuResponseFactory.DoAction("/default", message);
                    await Bot.SendTextMessageAsync(
                        chatId,
                        dialog,
                        replyMarkup: keyboard);                   
                }
                else
                {
                    string result = Business.Link(message.From.Id, message.Contact.PhoneNumber);
                    //inform user to ask admin to link device
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        result,
                        replyMarkup: new ReplyKeyboardRemove());
                }               
            }
            else
            {
                //do the menu
                ReplyKeyboardMarkup replyKeyboard = new[]
                {
                    new[] { "/menu", "/shipment" },                   
                };

                var consignee = Business.GetConsigneeRegisteredTo(message.From.Id);
                await Bot.SendTextMessageAsync(
                    message.Chat.Id,
                    consignee,
                    replyMarkup: replyKeyboard);
            }
        }

        private static async void DoReply(Telegram.Bot.Types.Message message)
        {
            if (String.CompareOrdinal(message.Text, "/start") == 0)
                DoDeviceLinking(message);
            else
            {
                var (keyboard, chatId, dialog) = MenuResponseFactory.DoAction(message.Text, message);
                await Bot.SendTextMessageAsync(
                    chatId,
                    dialog,
                    replyMarkup: keyboard);
            }
        }

        private static async void DoDefault(Telegram.Bot.Types.Message message)
        {
            var (keyboard, chatId, dialog) = MenuResponseFactory.DoAction("/default", message);
            await Bot.SendTextMessageAsync(
                chatId,
                dialog,
                replyMarkup: keyboard);
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null) return;
            //save a reference of chat id against selected shipment ->
            try
            {

                switch (message.Type)
                {                    
                    case MessageType.Text:
                        DoReply(message);
                        break;
                    case MessageType.Contact:
                        DoDeviceLinking(message);
                        break;
                    case MessageType.Unknown:                        
                    default:
                        DoDefault(message);
                        break;
                }              
                
            }
            catch (Exception ex)
            {
                await Bot.SendTextMessageAsync(
                    message.Chat.Id, ex.Message,
                    replyMarkup: new ReplyKeyboardRemove());
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender,
            CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            var concatenatedString = callbackQuery.Data;
            var split = concatenatedString.Split(';');
            await Bot.SendLocationAsync(
                callbackQuery.From.Id,
                float.Parse(split[0]),
                float.Parse(split[1]),
                0,
                true,
                replyMarkup: new ReplyKeyboardRemove()
                );

            await Bot.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"Status-> {split[2]}",
                replyMarkup: new ReplyKeyboardRemove());
        }

        private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
    }
}
