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

namespace TelegrambotZ
{
    internal class Program
    {

        
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString);
            sqlConnection.Open();

            String token = "6614760387:AAHV4WM9H35p5a9bN2ikYfWmfliooJoXCq0";
            var client = new TelegramBotClient(token);
            client.StartReceiving(Update, Error);
            
         
            Console.ReadLine();


        }
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                await Console.Out.WriteLineAsync("Успешное подключение");
            }
            string name1 = null;
            string nickName1 = null;
            int number1 = 0;
            var message = update.Message;
            
            if(message.Text != null)
            {
                if (message.Text.ToLower().Contains("/start"))
                {
                   await botClient.SendTextMessageAsync(message.Chat.Id, "Здарова, ты попал в Темясовский бот, здесь ты можешь работать и пить пиво.\nЧтобы зарегистрироваться введите /register");
                    return;
                    
                }
                if (message.Text.ToLower().Contains("/register"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Напишите игровой ник:");
                   
                   
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Users (Name, NickName, Number) VALUES (@Name,@NickName,@Number)",sqlConnection );
                    cmd.Parameters.AddWithValue("@Name", message.Chat.FirstName);
                    cmd.Parameters.AddWithValue("@NickName", message.Chat.Username);
                    cmd.Parameters.AddWithValue("@Number", $"1");
                    await Console.Out.WriteLineAsync(cmd.ExecuteNonQuery().ToString());
                    return;


                }
                if (message.Text.ToLower().Contains("/users"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Список зарегистрированных пользователей:");
                    number1 += 1;
                    SqlCommand cmd1 = new SqlCommand($"SELECT Name FROM dbo.users", sqlConnection);
                    
                    using (SqlDataReader rdr = cmd1.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var myString = rdr.GetString(0);
                            // Do somthing with this rows string, for example to put them in to a list
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{myString}");
                        }
                        return;
                    }



                }

            }

        }

        private static Task Error(ITelegramBotClient arg1, Exception exception, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}
