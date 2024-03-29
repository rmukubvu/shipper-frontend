﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;



namespace ZimconBotTelegram
{
    class MainClass
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient(ConfigurationManager.AppSettings["telegram.token"]);
        private static readonly Business Business = new Business();
        private static readonly MenuResponseFactory MenuResponseFactory = new MenuResponseFactory();

        public static void Main(string[] args)
        {
            var me = Bot.GetMeAsync().Result;
            Console.Title = me.Username;
            Business.CacheDevices();
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
           // Bot.OnInlineQuery += BotOnInlineQueryReceived;
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
                //await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                Console.WriteLine($"From: {message.From.Username} -> {message.From.FirstName}");
                //register this device
                Business.SaveDevice(message.From.Id);
                //inform user to ask admin to link device
                await Bot.SendTextMessageAsync(
                    message.Chat.Id,
                    "Device is not linked to a truck , please take it to your supervisor.",
                    replyMarkup: new ReplyKeyboardRemove());
            }
            else
            {
                //do the menu
                ReplyKeyboardMarkup replyKeyboard = new[]
                {
                    new[] { "/menu", "/location" },
                    //new[] { "/status", "/location" },
                };

                var vehicleInfor = Business.GetVehicleInfor(message.From.Id);
                await Bot.SendTextMessageAsync(
                    message.Chat.Id,
                    vehicleInfor,
                    replyMarkup: replyKeyboard);
            }
        }

        private static async void DoLocationUpdates(Telegram.Bot.Types.Message message)
        {
            if (message.Type != MessageType.Location) return;
            await Task.Run(() => {
                Business.SaveLocation(message.From.Id, message.Location.Latitude, message.Location.Longitude);
                Console.WriteLine($"{message.Location.Latitude},{message.Location.Longitude}");
            });
           
            await Bot.SendLocationAsync(
                       message.From.Id,
                       message.Location.Latitude,
                       message.Location.Longitude,
                       60,
                       replyMarkup: new ReplyKeyboardRemove()
                       );
           
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null) return;
            //save a reference of chat id against selected shipment ->
            try
            {
                //do location updates
                DoLocationUpdates(message);
                //do menu updates
                if ( message.Text == null ) return;
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
            var statusId = int.Parse(callbackQuery.Data);
            //await Task.Run(() =>
            //{
                Console.WriteLine("doing something {0}", statusId);
                //Business.SaveShipmentStatus(callbackQuery.From.Id, statusId);
            //});
            //to handle menu responses here ...
            await Bot.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                "Thank you for updating the status");
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

