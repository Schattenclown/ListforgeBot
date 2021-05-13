using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotDLL
{
    public class TelegramBot
    {
        static bool debug = false;
        static ITelegramBotClient botClient;
        static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        static List<TLG_Userdata> lstud = new List<TLG_Userdata>();
        private const string db = "LF_ServerInfoLive";
        private static string token = "";
        private static int virgin = 0;
        public static void Main()
        {
            if(virgin == 0)
            {
                Connections connections = Connections.GetConnections();
                token = connections.TelegramBotKey;
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

            var me = botClient.GetMeAsync().Result;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            if (debug == true)
                InformME();

        }
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            lstlive = LF_ServerInfo.ReadAll(db);
            String message;
            String servers = "";
            String addme = "";
            String delme = "";

            foreach (var item in lstlive)
            {
                servers += "/" + item.Name + "\n";
                addme += "/add" + item.Name + "\n";
                delme += "/del" + item.Name + "\n";
            }

            if (debug == false)
                await botClient.SendTextMessageAsync(chatId: 1038648681, text: $"Connection established with\nUsername: {e.Message.Chat.Username}\nChatID: {e.Message.Chat.Id}\nMessage was: {e.Message.Text}\n");

            switch (e.Message.Text.ToLower())
            {
                case "/help":
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: $"Type /help for help obviously!\n" +
                                                                                          $"\n" +
                                                                                          $"/42 All ServerInfo.!\n" +
                                                                                          $"/42lk All Serverinfo. but lowkey!\n" +
                                                                                          $"\n" +
                                                                                          $"{servers}\n" +
                                                                                          $"/add Be informed if serverstats change!\n" +
                                                                                          $"/del Unsubscribe from serverstats!\n" +
                                                                                          $"/abo What am i subscribed to!\n" +
                                                                                          $"\n" +
                                                                                          $"(✿◠‿◠) thx for using me!");
                    break;
                case "/add":
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: $"About wich Servers do you want to get Informed?\n\n{addme}");
                    break;
                case "/del":
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: $"What server do you wish to Silence?\n\n{delme}");
                    break;
                case "/42":
                    Send42(Convert.ToInt32(e.Message.Chat.Id), false);
                    break;
                case "/42lk":
                    Send42(Convert.ToInt32(e.Message.Chat.Id), true);
                    break;
                case "/abo":
                    AboutMe(Convert.ToInt32(e.Message.Chat.Id));
                    break;
                default:
                    bool np = false;
                    foreach (var item in lstlive)
                    {
                        if (e.Message.Text == $"/{item.Name}")
                        {
                            message = GetRightInfo(e.Message.Text.Trim('/'));
                            await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: message);
                            np = true;
                            break;
                        }
                        else if (e.Message.Text == $"/add{item.Name}")
                        {
                            ChangeInform(item.Name, Convert.ToInt32(e.Message.Chat.Id), e.Message.Chat.Username, true, false);
                            np = true;
                            break;
                        }
                        else if (e.Message.Text == $"/del{item.Name}")
                        {
                            ChangeInform(item.Name, Convert.ToInt32(e.Message.Chat.Id), e.Message.Chat.Username, false, false);
                            np = true;
                            break;
                        }
                    }
                    if (np == false)
                    {
                        message = $"Sry cant help ( ´･･)ﾉ(._.`)\n" +
                                  $"Type /help";
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: message);

                    }
                    break;
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
        public static async void DidChangePlayerCount(LF_ServerInfo obj)
        {
            lstud = TLG_Userdata.ReadAll();

            foreach (var item in lstud)
            {
                if (item.Abo == true && item.ServerId == obj.Id)
                {
                    await botClient.SendTextMessageAsync(chatId: item.ChatId, text: $"Name: /{obj.Name}\n" +
                                                                               $"IP: {obj.Address}:{obj.Port}\n" +
                                                                               $"Player count changed to: {obj.Players}/{obj.Maxplayers}");
                }
            }
        }
        public static async void DidChangeStatus(LF_ServerInfo obj)
        {
            String sonoff = "Offline";
            if (obj.Is_online == true)
                sonoff = "Online";

            lstud = TLG_Userdata.ReadAll();

            foreach (var item in lstud)
            {
                if (item.Abo == true && item.ServerId == obj.Id)
                {
                    await botClient.SendTextMessageAsync(chatId: item.ChatId, text: $"Name: /{obj.Name}\n" +
                                                                               $"IP: {obj.Address}:{obj.Port}\n" +
                                                                               $"Status changed to: {sonoff}");
                }
            }
        }
        public static String GetRightInfo(String message)
        {
            String rightinfo = "";

            foreach (var item in lstlive)
            {
                if (item.Name == message)
                {
                    String onoff = "Offline";
                    if (item.Is_online == true)
                        onoff = "Online";
                    try
                    {
                        if (item.Name != "TheForest")
                            rightinfo = $"Name: {item.Name}\n" +
                                        $"IP: {item.Address}:{item.Port}\n" +
                                        $"Status: {onoff}\n" +
                                        $"Players: {item.Players}/{item.Maxplayers}\n" +
                                        $"Version {item.Version}\n" +
                                        $"Uptime: {item.Uptime}%\n" +
                                        $"{item.LF_Uri.AbsoluteUri}\n" +
                                        $"Last check: {item.Last_check}\n" +
                                        $"Last online: {item.Last_online}";
                        else
                            rightinfo = $"Name: {item.Name}\n" +
                                    $"IP: {item.Address}:{item.Port}\n" +
                                    $"Status: {onoff}\n" +
                                    $"Players: {item.Players}/{item.Maxplayers}\n" +
                                    $"Version {item.Version.Replace("theforestDS ", "")}\n" +
                                    $"Uptime: {item.Uptime}%\n" +
                                    $"Last check: {item.Last_check}\n" +
                                    $"Last online: {item.Last_online}";
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return rightinfo;
        }
        static async void ChangeInform(string name, int chatid, String username, bool abo, bool notification)
        {
            int serverid = 0;
            String sabo = $"You will get notifications for {name}!";
            if (abo == false)
                sabo = $"You wont get notified for {name} anymore!";
            lstud = TLG_Userdata.ReadAll();

            foreach (var item in lstlive)
            {
                if (item.Name == name.Replace("/add", "").Replace("/del", ""))
                {
                    serverid = item.Id;
                }
            }

            bool found = false;
            foreach (var item in lstud)
            {
                if (item.ChatId == chatid && item.ServerId == serverid)
                    found = true;
            }

            if (found == false)
            {
                if (abo == true)
                {
                    TLG_Userdata ud = new TLG_Userdata
                    {
                        ChatId = Convert.ToInt32(chatid),
                        ServerId = serverid,
                        Abo = abo,
                    };
                    if (username != null)
                        ud.Username = username;
                    else
                        ud.Username = "Noname";
                    TLG_Userdata.Add(ud, notification);
                    await botClient.SendTextMessageAsync(chatId: chatid, text: sabo);
                }
            }
            else if (found == true)
            {
                {
                    TLG_Userdata ud = new TLG_Userdata
                    {
                        ChatId = Convert.ToInt32(chatid),
                        ServerId = serverid,
                        Abo = abo
                    };
                    if (username != null)
                        ud.Username = username;
                    else
                        ud.Username = "Noname";
                    TLG_Userdata.Change(ud, notification);
                    await botClient.SendTextMessageAsync(chatId: chatid, text: sabo);
                }
            }
        }
        static async void AboutMe(int chatid)
        {
            lstud = DB_TL_Userdata.ReadAll();
            lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var si in lstlive)
            {
                foreach (var ud in lstud)
                {
                    if (ud.ChatId == chatid && ud.Abo == true && ud.ServerId == si.Id)
                        await botClient.SendTextMessageAsync(chatId: chatid, text: $"{si.Name}");
                }
            }
        }
        private async static void Send42(int chatId, bool lowkey)
        {
            foreach (var item in lstlive)
            {
                String onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                if (item.Name != "TheForest" && lowkey == false)
                    await botClient.SendTextMessageAsync(chatId: chatId, text: $"Name: {item.Name}\n" +
                                                                                $"IP: {item.Address}:{item.Port}\n" +
                                                                                $"Status: {onoff}\n" +
                                                                                $"Players: {item.Players}/{item.Maxplayers}\n" +
                                                                                $"Version {item.Version}\n" +
                                                                                $"Uptime: {item.Uptime}%\n" +
                                                                                $"{item.LF_Uri}\n" +
                                                                                $"Last check: {item.Last_check}\n" +
                                                                                $"Last online: {item.Last_online}\n");
                else if (lowkey == false)
                    await botClient.SendTextMessageAsync(chatId: chatId, text: $"Name: {item.Name}\n" +
                                                                                $"IP: {item.Address}:{item.Port}\n" +
                                                                                $"Status: {onoff}\n" +
                                                                                $"Players: {item.Players}/{item.Maxplayers}\n" +
                                                                                $"Version {item.Version.Replace("theforestDS ", "")}\n" +
                                                                                $"Uptime: {item.Uptime}%\n" +
                                                                                $"Last check: {item.Last_check}\n" +
                                                                                $"Last online: {item.Last_online}\n");
                else if (item.Name != "TheForest" && lowkey == true)
                    await botClient.SendTextMessageAsync(chatId: chatId, text: $"Name: {item.Name}\n" +
                                                                                $"IP: {item.Address}:{item.Port}\n" +
                                                                                $"Status: {onoff}\n" +
                                                                                $"Players: {item.Players}/{item.Maxplayers}\n");
                else if (lowkey == true)
                    await botClient.SendTextMessageAsync(chatId: chatId, text: $"Name: {item.Name}\n" +
                                                                                $"IP: {item.Address}:{item.Port}\n" +
                                                                                $"Status: {onoff}\n" +
                                                                                $"Players: {item.Players}/{item.Maxplayers}\n");
            }
        }
    }
}