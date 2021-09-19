using System;
using System.Threading.Tasks;

using DisCatSharp;
using DisCatSharp.CommandsNext;
using DisCatSharp.EventArgs;

using static BotDLL.DiscordBot;

namespace BotDLL.EventListener
{
    /// <summary>
    /// The client events.
    /// </summary>
    class ClientEvents
    {
        public static Task CNext_CommandErrored(CommandsNextExtension ex, CommandErrorEventArgs e)
        {
            if (e.Command == null)
            {
                Console.WriteLine($"{e.Exception.Message}");
            }
            else
            {
                Console.WriteLine($"{e.Command.Name}: {e.Exception.Message}");
            }
            return Task.CompletedTask;
        }

        public static Task Client_SocketErrored(DiscordClient dcl, SocketErrorEventArgs e)
        {
            Main.Bot.running = false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (Main.Bot.debug) Console.WriteLine("Socket has an error! " + e.Exception.Message.ToString());
            return Task.CompletedTask;
        }

        public static Task Client_SocketClosed(DiscordClient dcl, SocketCloseEventArgs e)
        {
            Main.Bot.running = false;
            Console.ForegroundColor = ConsoleColor.Red;
            if (Main.Bot.debug) Console.WriteLine("Socket closed: " + e.CloseMessage);
            return Task.CompletedTask;
        }

        public static Task Client_Heartbeated(DiscordClient dcl, HeartbeatEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (Main.Bot.debug) Console.WriteLine("Received Heartbeat:" + e.Ping);
            Console.ForegroundColor = ConsoleColor.Gray;
            return Task.CompletedTask;
        }
    }
}
