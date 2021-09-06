using BotDLL.Model.BotCom.DiscordCommands;
using BotDLL.Model.BotCom.DiscordCommands.Base;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotDLL
{
    public class DiscordBot
    {

        private static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        static List<DC_Userdata> lstud = new List<DC_Userdata>();
        private const string db = "LF_ServerInfoLive";
        private static DiscordSocketClient _client;
        private static string token = "";
        private static int virgin = 0;

        public static async Task Init()
        {
            if (virgin == 0)
            {
                Connections connections = Connections.GetConnections();
                token = connections.DiscordBotKey;
                virgin = 69;
            }

            _client = new DiscordSocketClient();
            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.MessageReceived += MessageReceivedAsync;
        }

        private static async Task MessageReceivedAsync(SocketMessage arg)
        {
            lstlive = LF_ServerInfo.ReadAll(db);
            string servers = "";
            string statisticsservers = "";
            String addme = "";
            String delme = "";

            foreach (var item in lstlive)
            {
                servers += "/" + item.Name.ToLower() + "\n";
                statisticsservers += "/s" + item.Name.ToLower() + "\n";
                addme += "/add" + item.Name + "\n";
                delme += "/del" + item.Name + "\n";
            }

            var eb = new EmbedBuilder();

            if (!arg.Author.IsBot)
            {
                IDiscordCommandAsync command;
                switch (arg.Content.ToLower())
                {
                    case "/test":
                        command = null;
                        LF_ServerInfo obj = lstlive[0];
                        DCChange(obj, "player");
                        DCChange(obj, "status");
                        DCChange(obj, "version");
                        break;
                    case "/help":
                        command = new HelpCommand();
                        break;
                    case "/list":
                        command = new ListCommand(servers);
                        break;
                    case "/statlist":
                        command = new StatListCommand(statisticsservers);
                        break;
                    case "/42big":
                        command = new ShowAllCommand(arg, lstlive);
                        break;
                    case "/42":
                        command = new ShowSomeCommand(arg, lstlive);
                        break;
                    case "/42s":
                        command = new ShowStatsCommand(arg, lstlive);
                        break;
                    case "/add":
                        command = new AddCommand(addme);
                        break;
                    case "/del":
                        command = new DeleteCommand(delme);
                        break;
                    case "/abo":
                        lstud = DB_DC_Userdata.ReadAll();
                        lstlive = LF_ServerInfo.ReadAll(db);
                        command = new AboutMeCommand(lstud, lstlive, arg);
                        break;
                    default:
                        command = null;
                        foreach (var item in lstlive)
                        {
                            if (arg.Content.ToLower() == $"/{item.Name.ToLower()}")
                            {
                                command = new ShowOneCommand(arg, lstlive);
                                break;
                            }
                            else if (arg.Content.ToLower() == $"/add{item.Name.ToLower()}")
                            {
                                lstud = DC_Userdata.ReadAll();
                                command = new ChangeSubscriptionCommand(lstud, lstlive, arg, item.Name, true);
                                break;
                            }
                            else if (arg.Content.ToLower() == $"/del{item.Name.ToLower()}")
                            {
                                lstud = DC_Userdata.ReadAll();
                                command = new ChangeSubscriptionCommand(lstud, lstlive, arg, item.Name, false);
                                break;
                            }
                            else if (arg.Content.ToLower() == $"/s{item.Name.ToLower()}")
                            {
                                command = new ShowStatsCommand(arg, item);
                            }
                            else if (arg.Content.ToLower() == "/addall")
                            {
                                lstlive = LF_ServerInfo.ReadAll(db);

                                foreach (var item1 in lstlive)
                                {
                                    command = new ChangeSubscriptionCommand(lstud, lstlive, arg, item1.Name, true);
                                    await command.Execute();
                                }
                                break;
                            }
                        }
                        break;
                }

                if (command != null)
                {
                    Embed message = await command.GetMessage();
                    if (message != null)
                        await arg.Channel.SendMessageAsync(null, false, message);

                    await command.Execute();
                }
            }
        }
        public static async void DCChange(LF_ServerInfo obj, string whatchanged)
        {
            lstud = DC_Userdata.ReadAll();
            List<UInt64> differentchannel = new List<ulong>();
            bool once = false;

            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Color = new Color(245, 107, 0)
            };
            embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
            embedBuilder.WithAuthor("ListforgeNotify abo");
            embedBuilder.WithFooter("(✿◠‿◠) thanks for using me");
            embedBuilder.WithTimestamp(DateTime.Now);

            foreach (var item in lstud)
            {
                if (item.Abo && item.ServerId == obj.Id)
                {
                    if(!once)
                    {
                        embedBuilder.AddField($"Name", $"{obj.Name}");
                        embedBuilder.AddField("Ip address", $"{obj.Address}:{obj.Port}");
                        if( whatchanged == "player")
                        { 
                            embedBuilder.Title = "Player count changed!";
                            embedBuilder.AddField("Player count changed to ", $"{obj.Players}/{obj.Maxplayers}");
                        }
                        else if (whatchanged == "status")
                        {
                            embedBuilder.Title = "Status changed!";
                            string sonoff = "Offline";
                            if (obj.Is_online)
                                sonoff = "Online";
                            embedBuilder.AddField("Status changed to ", $"{sonoff}");
                        }
                        else if(whatchanged == "version")
                        {
                            embedBuilder.Title = "Version changed!";
                            embedBuilder.AddField("Serverversion changed to ", $"{obj.Version}");
                        }

                        once = true;
                    }
                    if (!differentchannel.Contains(Convert.ToUInt64(item.ChannelId)))
                        differentchannel.Add(Convert.ToUInt64(item.ChannelId));
                }
            }

            string tags = "";

            foreach (var item in differentchannel)
            {
                foreach (var item2 in lstud)
                {
                    if (!tags.Contains(item2.AuthorId) && item == Convert.ToUInt64(item2.ChannelId) && item2.Abo && item2.ServerId == obj.Id)
                        tags += "<@" + item2.AuthorId + ">" + "\n";
                }
                
                embedBuilder.WithDescription(tags);

                ISocketMessageChannel chn = _client.GetChannel(item) as ISocketMessageChannel;
            
                if(chn != null)
                    await chn.SendMessageAsync(null, false, embedBuilder.Build(), null);

                tags = "";
            }
        }

        private static void Center(string s)
        {
            try
            {
                Console.Write("██");
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.Write(s);
                Console.SetCursorPosition((Console.WindowWidth - 4), Console.CursorTop);
                Console.WriteLine("██");
            }
            catch (Exception)
            {
                Console.WriteLine("Console to small");

            }
        }

        private static Task Log(LogMessage msg)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                Center($"DISCORD: {msg.ToString()}");
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
            }
            catch (Exception)
            {
                Console.WriteLine("Console to small");
            }

            return Task.CompletedTask;
        }
    }
}
