using BotDLL.Model.BotCom.TelegramCommands;
using BotDLL.Model.BotCom.TelegramCommands.Base;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace BotDLL.Model.BotCom
{
    /// <summary>
    /// The telegram bot.
    /// </summary>
    public class TelegramBot
    {
        static bool debug = false;
        static ITelegramBotClient botClient;
        static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        static List<TL_Userdata> lstud = new List<TL_Userdata>();
        /// <summary>
        /// The db.
        /// </summary>
        private const string db = "LF_ServerInfoLive";
        private static string token = "";
        private static int virgin = 0;
        public static async Task Init()
        {
            if (virgin == 0)
            {
                Connections connections = Connections.GetConnections();
                token = connections.TelegramBotKey;
#if DEBUG
                token = connections.TelegramBotKeyDebug;
#endif
                virgin = 69;
            }

            bool con = false;
            while (con == false)
            {
                try
                {
                    botClient = new TelegramBotClient(token);
                    con = true;
                }
                catch (Exception)
                {
                    ConProblem();
                }
            }

            var me = await botClient.GetMeAsync();
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            if (debug == true)
                InformME();

        }
        /// <summary>
        /// On message event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            lstlive = LF_ServerInfo.ReadAll(db);
            String message;
            String servers = "";
            string statisticsservers = "";
            String addme = "";
            String delme = "";

            foreach (var item in lstlive)
            {
                servers += "/" + item.Name + "\n";
                statisticsservers += "/S" + item.Name + "\n";
                addme += "/add" + item.Name + "\n";
                delme += "/del" + item.Name + "\n";
            }

            if (debug == true)
                await botClient.SendTextMessageAsync(chatId: 1038648681, text: $"Connection established with\nUsername: {e.Message.Chat.Username}\nChatID: {e.Message.Chat.Id}\nMessage was: {e.Message.Text}\n");

            ITelegramCommandAsync command = null;

            switch (e.Message.Text.ToLower())
            {
                case "/help":
                    command = new HelpCommand(servers, statisticsservers);
                    break;
                case "/add":
                    command = new AddCommand(addme);
                    break;
                case "/del":
                    command = new DeleteCommand(delme);
                    break;
                case "/42big":
                    command = new ShowAllCommand(botClient, e.Message.Chat.Id, lstlive);
                    break;
                case "/42":
                    command = new ShowSomeCommand(botClient, e.Message.Chat.Id, lstlive);
                    break;
                case "/42s":
                    command = new ShowStatsCommand(botClient, e.Message.Chat.Id, lstlive);
                    break;
                case "/abo":
                    lstud = DB_TL_Userdata.ReadAll();
                    lstlive = LF_ServerInfo.ReadAll(db);

                    command = new AboutMeCommand(botClient, lstud, lstlive, e.Message.Chat.Id);
                    break;
                default:
                    foreach (var item in lstlive)
                    {
                        if (e.Message.Text == $"/{item.Name}")
                        {
                            command = new ShowOneCommand(botClient, e.Message.Chat.Id, lstlive, e.Message.Text.Trim('/'));
                            break;
                        }
                        else if (e.Message.Text == $"/add{item.Name}")
                        {
                            lstud = TL_Userdata.ReadAll();

                            command = new ChangeSubscriptionCommand(botClient, lstud, lstlive, item.Name, e.Message.Chat.Id, e.Message.Chat.Username, true);
                            break;
                        }
                        else if (e.Message.Text == $"/del{item.Name}")
                        {
                            lstud = TL_Userdata.ReadAll();

                            command = new ChangeSubscriptionCommand(botClient, lstud, lstlive, item.Name, e.Message.Chat.Id, e.Message.Chat.Username, false);
                            break;
                        }
                    }
                    break;
            }

            string outputMessage = command != null ? await command.GetMessage() : string.Empty;
            if (outputMessage != string.Empty)
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: outputMessage);

            if (command == null || !await command.Execute())
            {
                message = $"Sry cant help ( ´･･)ﾉ(._.`)\n" +
                          $"Type /help";
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: message);
            }
        }
        private async static void InformME()
        {
            await botClient.SendTextMessageAsync(chatId: 1038648681, text: $"Im Online/Back Online?");
        }
        private async static void ConProblem()
        {
            await botClient.SendTextMessageAsync(chatId: 1038648681, text: $"I had a connection Problem!");
        }
        public static async void TGChange(LF_ServerInfo obj, string whatchanged)
        {
            lstud = TL_Userdata.ReadAll();

            if (whatchanged == "player")
            {
                foreach (var item in lstud)
                {
                    if (item.Abo && item.ServerId == obj.Id)
                    {
                        await botClient.SendTextMessageAsync(chatId: item.ChatId, text: $"Name: /{obj.Name}\n" +
                                                                                   $"IP: {obj.Address}:{obj.Port}\n" +
                                                                                   $"Player count changed to: {obj.Players}/{obj.Maxplayers}");
                    }
                }
            }
            else if (whatchanged == "status")
            {
                string sonoff = "Offline";
                if (obj.Is_online)
                    sonoff = "Online";

                foreach (var item in lstud)
                {
                    if (item.Abo && item.ServerId == obj.Id)
                    {
                        await botClient.SendTextMessageAsync(chatId: item.ChatId, text: $"Name: /{obj.Name}\n" +
                                                                                   $"IP: {obj.Address}:{obj.Port}\n" +
                                                                                   $"Status changed to: {sonoff}");
                    }
                }
            }
            else if (whatchanged == "version")
            {
                foreach (var item in lstud)
                {
                    if (item.Abo && item.ServerId == obj.Id)
                    {
                        await botClient.SendTextMessageAsync(chatId: item.ChatId, text: $"Name: /{obj.Name}\n" +
                                                                                   $"IP: {obj.Address}:{obj.Port}\n" +
                                                                                   $"Serverversion changed to: {obj.Version}");
                    }
                }
            }
        }
    }
}