using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace BitkeshTelegram
{
    class MainClass
    {

        private static readonly TelegramBotClient Bot = new TelegramBotClient("757259151:AAGYpmYFuYxMQ1wNxEBO2E2IJ3S74BuDhfU");
        private static InMemoryDatabase inMemory;

        public static void Main(string[] args)
        {
            var me = Bot.GetMeAsync().Result;
            Console.Title = me.Username;
            inMemory = new InMemoryDatabase();
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

        public static async void DownloadFile(Telegram.Bot.Types.Message message)
        {
            /*try
            {
                var file = await Bot.GetFileAsync(message.Photo.LastOrDefault().FileId);
                var filename = file.FileId + "." + file.FilePath.Split('.').Last();
                using (var saveImageStream = new FileStream(filename, (System.IO.FileAccess)FileMode.Create))
                {
                    await file..CopyToAsync(saveImageStream);
                }
            }
            catch (Exception)
            {
                throw;
            }*/
        }


        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null) return;


            if ( message.Type == MessageType.Photo ){
                //message.Pho
                //DownloadFile(message);
                await Bot.SendTextMessageAsync(
                       message.Chat.Id,
                       "Bitkesh - Payments made easy\n"+
                       "Thank you we will be in touch soon",
                       replyMarkup: new ReplyKeyboardRemove());
            }


            if ( message.Type == MessageType.Contact ){
                var phoneNumber = message.Contact.PhoneNumber;
                //check if account holder
                if ( inMemory.ValidatePhoneNumber(phoneNumber) != 0 ){
                    inMemory.RegisterMobileToChatId(phoneNumber, message.From.Id);
                    //good good
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(500); // simulate longer running task
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Type in your pin number followed by # to continue",
                        replyMarkup: new ReplyKeyboardRemove());

                }else{
                    const string usage = "Upload your photo to be\n" +
                                          "able to be registered onto \n" +
                                          "the system.";
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        usage,
                        replyMarkup: new ReplyKeyboardRemove());
                }
            }

            if (message.Type == MessageType.Text)
            {

                if (message.Text.Contains("#")){ // its the pin message

                    var pin = int.Parse(message.Text.Replace("#", ""));
                    if (inMemory.ValidateLoginsUsingChatId(message.From.Id,pin)){
                        ReplyKeyboardMarkup ReplyKeyboard = new[]
                        {
                            new[] { "/menu", "/transfer" },
                            new[] { "/dstv", "/zesa" },
                        };

                        await Bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "Bitkesh - Payments made easy\n" +
                            "Balance is R1,000 (20 tokens).\n" +
                            "Current rate is 1 token : R4.50",
                            replyMarkup: ReplyKeyboard);
                    }
                    else{
                        await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Invalid pin provided , please type in your pin number followed by # to continue",
                        replyMarkup: new ReplyKeyboardRemove());
                    }
                    return;
                }


                switch (message.Text.Split(' ').First())
                {
                    // send inline keyboard
                    case "/menu":                       
                        ReplyKeyboardMarkup ReplyKeyboard = new[]
                        {
                            new[] { "/menu", "/transfer" },
                            new[] { "/dstv", "/zesa" },
                        };

                        await Bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "Choose",
                            replyMarkup: ReplyKeyboard);

                        break;
                    case "/dstv":
                        await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                        await Task.Delay(500); // simulate longer running task
                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            new [] // first row
                            {
                                InlineKeyboardButton.WithCallbackData("Box Office"),
                                InlineKeyboardButton.WithCallbackData("Premium"),
                            },
                            new [] // second row
                            {
                                InlineKeyboardButton.WithCallbackData("Showmax"),
                                InlineKeyboardButton.WithCallbackData("Dstv Xtra"),
                            }
                        });

                        await Bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "Choose",
                            replyMarkup: inlineKeyboard);
                        break;

                    case "/transfer":
                        await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                        await Task.Delay(500); // simulate longer running task
                        var inlineKeyboard2 = new InlineKeyboardMarkup(new[]
                        {
                            new [] // first row
                            {
                                InlineKeyboardButton.WithCallbackData("To Zim"),
                                InlineKeyboardButton.WithCallbackData("To SA"),
                            },
                            new [] // second row
                            {
                                InlineKeyboardButton.WithCallbackData("From Zim"),
                                InlineKeyboardButton.WithCallbackData("From SA"),
                            }
                        });

                        await Bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "Choose",
                            replyMarkup: inlineKeyboard2);
                        break;
                    case "/zesa":
                        break;
                    default:
                        var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                        {
                           KeyboardButton.WithRequestContact("My Phone Number")                           
                        });

                        await Bot.SendTextMessageAsync(
                            message.Chat.Id,
                            "Bitkesh - Payments made easy. To start you need to provide us with your mobile number by clicking on button below [My Phone Number]",
                            replyMarkup: RequestReplyKeyboard);
                        break;                    

                }
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            Console.WriteLine(callbackQuery.Data);
            await Bot.AnswerCallbackQueryAsync(
                callbackQuery.Id,
                $"Received {callbackQuery.Data}");

            await Bot.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"Received {callbackQuery.Data}");
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
