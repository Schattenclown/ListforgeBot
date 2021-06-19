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

            foreach (var item in lstlive)
            {
                servers += "!" + item.Name.ToLower() + "\n";
                statisticsservers += "!s" + item.Name.ToLower() + "\n";
            }

            var eb = new EmbedBuilder();

            if (!arg.Author.IsBot)
            {
                IDiscordCommandAsync command;
                switch (arg.Content.ToLower())
                {
                    case "!help":
                        command = new HelpCommand(servers, statisticsservers);
                        break;
                    case "!list":
                        command = new ListCommand(servers);
                        break;
                    case "!42big":
                        command = new ShowAllCommand(arg, lstlive);
                        break;
                    case "!42":
                        command = new ShowSomeCommand(arg, lstlive);
                        break;
                    case "!42s":
                        command = new ShowStatusCommand(arg, lstlive);
                        break;
                    default:
                        command = new ShowOneCommand(arg, lstlive);
                        break;
                }

                if(command != null)
                {
                    Embed message = await command.GetMessage();
                    if(message != null)
                        await arg.Channel.SendMessageAsync(null, false, message);

                    await command.Execute();
                }
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
