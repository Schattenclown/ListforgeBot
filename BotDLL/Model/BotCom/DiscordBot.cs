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

        static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        private const string db = "LF_ServerInfoLive";
        private DiscordSocketClient _client;
        private static string token = "";
        private static int virgin = 0;
        public static void Main()
        {
            if(virgin == 0)
            {
                Connections connections = Connections.GetConnections();
                token = connections.DiscordBotKey;
                virgin = 69;
            }
            new DiscordBot().MainAsync().GetAwaiter().GetResult();
        }
        private Task Log(LogMessage msg)
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
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            var asynctoken = token;
            await _client.LoginAsync(TokenType.Bot, asynctoken);
            await _client.StartAsync();
            _client.MessageReceived += MessageReceivedAsync;
        }
        public async Task MessageReceivedAsync(SocketMessage arg)
        {
            lstlive = LF_ServerInfo.ReadAll(db);
            string servers = "";
            string statisticsservers = "";
            foreach (var item in lstlive)
            {
                servers += "!" + item.Name.ToLower() + "\n";
                statisticsservers += "!s" + item.Name.ToLower() + "\n";
            }
            var eb = new EmbedBuilder();
            if (!arg.Author.IsBot)
            {
                switch (arg.Content.ToLower())
                {
                    case "!help":
                        eb.Description = $"```!help     shows help\n" +
                                                $"\n" +
                                                $"!42big       show every server\n" +
                                                $"!42     show every server with less info\n" +
                                                $"!42s      show playerstatistics for every server\n" +
                                                $"\n" +
                                                $"!list     show serverlist\n" +
                                                $"\n" +
                                                $"Serverinfo from single server\n" +
                                                $"{servers}\n" +
                                                $"\n" +
                                                $"Playerstatistics from single server\n" +
                                                $"{statisticsservers}\n" +
                                                $"(✿◠‿◠) ty for using me```";
                        eb.Color = Color.LightOrange;
                        eb.Title = "cmd info";
                        await arg.Channel.SendMessageAsync(null, false, eb.Build());
                        break;
                    case "!list":
                        eb.Description = $"```{servers}```";
                        eb.Color = Color.LightOrange;
                        eb.Title = "Serverlist";
                        await arg.Channel.SendMessageAsync(null, false, eb.Build());
                        break;
                    case "!42big":
                        Send42(arg, "42big");
                        break;
                    case "!42":
                        Send42(arg, "42");
                        break;
                    case "!42s":
                        Send42(arg, "42s");
                        break;
                    default:
                        Sendright(arg);
                        break;
                }

            }

        }
        public async static void Sendright(SocketMessage arg)
        {
            if (!arg.Author.IsBot)
            {
                var eb = new EmbedBuilder();
                foreach (var item in lstlive)
                {
                    String onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    if (item.Name.ToLower() == arg.Content.ToLower().Trim('!'))
                    {
                        eb.Title = item.Name;
                        if (item.Name != "TheForest")
                            eb.Url = item.LF_Uri.AbsoluteUri;
                        eb.Description = $"```IP:           {item.Address}:{item.Port}\n" +
                                            $"Status:       {onoff}\n" +
                                            $"Players:      {item.Players}/{item.Maxplayers}\n" +
                                            $"Version:      {item.Version}\n" +
                                            $"Uptime:       {item.Uptime}%\n" +
                                            $"Last check:   {item.Last_check}\n";
                        eb.Color = Color.Green;
                        if (onoff == "Offline")
                        {
                            eb.Color = Color.Red;
                            eb.Description += $"Last online:  { item.Last_online}```";
                        }
                        else
                            eb.Description += "```";
                        if (item.LF_Uri.AbsoluteUri != null)
                            eb.ImageUrl = item.LF_Uri.AbsoluteUri;
                        await arg.Channel.SendMessageAsync(null, false, eb.Build());
                    }
                    else if ('s' + item.Name.ToLower() == arg.Content.ToLower().Trim('!'))
                    {
                        eb.Title = item.Name;
                        eb.Description = $"Player Statistics for {item.Name}";
                        eb.ImageUrl = item.QC_StatUri.AbsoluteUri;
                        if (item.Name != "TheForest")
                            eb.Url = item.LF_Uri.AbsoluteUri;
                        eb.Color = Color.Green;
                        if (onoff == "Offline")
                            eb.Color = Color.Red;

                        await arg.Channel.SendMessageAsync(null, false, eb.Build());
                    }
                }
            }
        }
        private async static void Send42(SocketMessage arg, string lowkey)
        {
            if (!arg.Author.IsBot)
            {
                var eb = new EmbedBuilder();
                foreach (var item in lstlive)
                {
                    string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";
                    
                    eb.Title = item.Name;
                    if (item.Name != "TheForest")
                        eb.Url = item.LF_Uri.AbsoluteUri;
                    if (lowkey == "42")
                    {
                        eb.Description = $"```IP:           {item.Address}:{item.Port}\n" +
                                        $"Status:       {onoff}\n" +
                                        $"Players:      {item.Players}/{item.Maxplayers}\n";
                        if (item.LF_Uri.AbsoluteUri != null)
                            eb.ImageUrl = item.LF_HeaderImgURi.AbsoluteUri;
                    }
                    else if (lowkey == "42s")
                    {
                        eb.Description = $"Player Statistics for {item.Name}";
                        eb.ImageUrl = item.QC_StatUri.AbsoluteUri;
                    }
                    else if (lowkey == "42big")
                    {
                        eb.Description = $"```IP:           {item.Address}:{item.Port}\n" +
                                            $"Status:       {onoff}\n" +
                                            $"Players:      {item.Players}/{item.Maxplayers}\n" +
                                            $"Version:      {item.Version}\n" +
                                            $"Uptime:       {item.Uptime}%\n" +
                                            $"Last check:   {item.Last_check}\n";
                        if (item.LF_Uri.AbsoluteUri != null)
                            eb.ImageUrl = item.LF_HeaderImgURi.AbsoluteUri;
                    }

                    eb.Color = Color.Green;
                    if (onoff == "Offline")
                        eb.Color = Color.Red;
                    
                    if (lowkey != "42s")
                        eb.Description += "```";
                    
                    await arg.Channel.SendMessageAsync(null, false, eb.Build());
                }
            }
        }
        static void Center(string s)
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
    }
}
