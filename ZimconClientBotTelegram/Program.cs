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
            Bot.OnInlineQuery += BotOnInlineQueryReceived;
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            Bot.OnReceiveError += BotOnReceiveError;
            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void DoDeviceLinking(Telegram.Bot.Types.Message message)
        {
            /*if (message.Type == MessageType.Contact)
            {
                var phoneNumber = message.Contact.PhoneNumber;
                //register this device
                business.SaveDevice(message.From.Id);
            }*/
            //do normal check
            //check device id is linked
            if (!Business.IsDeviceLinked(message.From.Id))
            {
                //await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
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

        private static async void DoLocationUpdates(Telegram.Bot.Types.Message message)
        {
            if (message.Type != MessageType.Location) return;
            await Task.Run(() => {
                Business.SaveLocation(message.From.Id, message.Location.Latitude, message.Location.Longitude);
            });
            await Bot.SendTextMessageAsync(
                message.Chat.Id, "Thank you for updating the location",
                replyMarkup: new ReplyKeyboardRemove());
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null) return;
            //save a reference of chat id against selected shipment ->
            try
            {

                //DoDeviceLinking(message);
                //do location updates
                //DoLocationUpdates(message);
                //do menu updates
                if (message.Text == null && message.Type != MessageType.Contact) return;
                var menuItem = message.Text.Split(' ').First();
                //do device linking
                if (String.CompareOrdinal(menuItem, "/start") == 0 ||
                    message.Type == MessageType.Contact)
                    DoDeviceLinking(message);
                else
                {
                    if (message.Type != MessageType.Text) return;
                    if (String.CompareOrdinal(menuItem, "/start") == 0) return;
                    var (keyboard, chatId, dialog) = MenuResponseFactory.DoAction(menuItem, message);                      
                    await Bot.SendTextMessageAsync(
                        chatId,
                        dialog,
                        replyMarkup: keyboard);
                    
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
            //make sure it does it only once ...
            /*var statusId = int.Parse(callbackQuery.Data);
            //await Task.Run(() =>
            //{
            Console.WriteLine("doing something {0}", statusId);
            //Business.SaveShipmentStatus(callbackQuery.From.Id, statusId);
            //});
            //to handle menu responses here ...
            */
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
                $"Status-> {split[2]}");
        }

        private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            Console.WriteLine($"Received inline query from: {inlineQueryEventArgs.InlineQuery.From.Id}");

            InlineQueryResultBase[] results = {
                new InlineQueryResultLocation(
                    id: "1",
                    latitude: 40.7058316f,
                    longitude: -74.2581888f,
                    title: "New York")   // displayed result
                    {
                        InputMessageContent = new InputLocationMessageContent(
                            latitude: 40.7058316f,
                            longitude: -74.2581888f)    // message if result is selected
                    },

                new InlineQueryResultLocation(
                    id: "2",
                    latitude: 13.1449577f,
                    longitude: 52.507629f,
                    title: "Berlin") // displayed result
                    {

                        InputMessageContent = new InputLocationMessageContent(
                            latitude: 13.1449577f,
                            longitude: 52.507629f)   // message if result is selected
                    }
            };

            await Bot.AnswerInlineQueryAsync(
                inlineQueryEventArgs.InlineQuery.Id,
                results,
                isPersonal: true,
                cacheTime: 0);
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
