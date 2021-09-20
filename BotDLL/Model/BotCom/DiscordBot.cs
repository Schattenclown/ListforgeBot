using DisCatSharp;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.CommandsNext;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using DisCatSharp.EventArgs;
using DisCatSharp.Interactivity;
using DisCatSharp.Interactivity.Enums;
using DisCatSharp.Interactivity.EventHandling;
using DisCatSharp.Interactivity.Extensions;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static BotDLL.Model.BotCom.Events.ClientEvents;
using static BotDLL.Model.BotCom.Events.GuildEvents;
using static BotDLL.Model.BotCom.Events.ApplicationCommandsEvents;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotDLL.Model.BotCom
{
    #region MultiDict
    /// <summary>
    /// Multidictionary
    /// </summary>
    /// <typeparam name="TKey">Key</typeparam>
    /// <typeparam name="TValue">Value</typeparam>
    public class MultiDict<TKey, TValue>
    {
        private readonly Dictionary<TKey, List<TValue>> _data = new Dictionary<TKey, List<TValue>>();

        /// <summary>
        /// Adds a <see cref="List{T}"/> to an <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="k">Key</param>
        /// <param name="v">Value</param>
        public void Add(TKey k, TValue v)
        {
            if (_data.ContainsKey(k))
                _data[k].Add(v);
            else
                _data.Add(k, new List<TValue>() { v });
        }

        /// <summary>
        /// Deletes a <see cref="List{T}"/> from  an <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="k">Key</param>
        /// <param name="v">Value</param>
        public void Del(TKey k, TValue v)
        {
            if (_data.ContainsKey(k))
                _data[k].Remove(v);
        }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <returns>Dictionary</returns>
        public Dictionary<TKey, List<TValue>> Get()
        {
            return _data;
        }
    }
    #endregion

    public class DiscordBot : IDisposable
    { /* möpse
        ( . )( . )
         )      (
        (   oo   )
         |  ||  | 
         |  V   |
       */



        private static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        static List<DC_Userdata> lstud = new List<DC_Userdata>();
        public const string db = "LF_ServerInfoLive";
        public static string token = "";
        public static int virgin = 0;
        public static DiscordClient Client { get; internal set; }
        public static ApplicationCommandsExtension ApplicationCommands { get; internal set; }
        public static CommandsNextExtension CNext { get; internal set; }
        public static InteractivityExtension INext { get; internal set; }
        public static CancellationTokenSource ShutdownRequest;
        public static readonly ulong testguild = 881868642600505354;
        public static string prefix = "lfn/";

        /// <summary>
        /// Binarie to text.
        /// </summary>
        /// <param name="data">The binary data.</param>
        public static string BinaryToText(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public DiscordBot()
        {
            if (virgin == 0)
            {
                Connections connections = Connections.GetConnections();
                token = connections.DiscordBotKey;
                virgin = 69;
            }
            ShutdownRequest = new CancellationTokenSource();

            LogLevel logLevel;
#if DEBUG
            logLevel = LogLevel.Debug;
#else
            logLevel = LogLevel.Error;
#endif
            var cfg = new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MessageCacheSize = 2048,
                MinimumLogLevel = logLevel,
                ShardCount = 1,
                ShardId = 0,
                Intents = DiscordIntents.AllUnprivileged,
                MobileStatus = false,
                UseCanary = false,
                AutoRefreshChannelCache = false
            };

            Client = new DiscordClient(cfg);
            ApplicationCommands = Client.UseApplicationCommands();
            CNext = Client.UseCommandsNext(new CommandsNextConfiguration {
                StringPrefixes = new string[] { prefix },
                CaseSensitive = true,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
                DefaultHelpChecks = null,
                EnableDefaultHelp = true,
                EnableDms = true
            });

            INext = Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2),
                PaginationBehaviour = PaginationBehaviour.WrapAround,
                PaginationDeletion = PaginationDeletion.DeleteEmojis,
                PollBehaviour = PollBehaviour.DeleteEmojis,
                AckPaginationButtons = true,
                ButtonBehavior = ButtonPaginationBehavior.Disable,
                PaginationButtons = new PaginationButtons()
                {
                    SkipLeft = new DiscordButtonComponent(ButtonStyle.Primary, "pgb-skip-left", "First", false, new DiscordComponentEmoji("⏮️")),
                    Left = new DiscordButtonComponent(ButtonStyle.Primary, "pgb-left", "Previous", false, new DiscordComponentEmoji("◀️")),
                    Stop = new DiscordButtonComponent(ButtonStyle.Danger, "pgb-stop", "Cancel", false, new DiscordComponentEmoji("⏹️")),
                    Right = new DiscordButtonComponent(ButtonStyle.Primary, "pgb-right", "Next", false, new DiscordComponentEmoji("▶️")),
                    SkipRight = new DiscordButtonComponent(ButtonStyle.Primary, "pgb-skip-right", "Last", false, new DiscordComponentEmoji("⏭️"))
                },
                ResponseMessage = "Something went wrong.",
                ResponseBehavior = InteractionResponseBehavior.Respond
            });

            RegisterEventListener(Client, ApplicationCommands, CNext);
            RegisterCommands(CNext, ApplicationCommands);

        }
        public void Dispose()
        {
            Client.Dispose();
            INext = null;
            CNext = null;
            Client = null;
            ApplicationCommands = null;
        }

        public async Task RunAsync()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            await Client.ConnectAsync();
            Console.WriteLine($"Starting with Prefix {prefix}");
            Console.WriteLine($"Starting {Client.CurrentUser.Username}");
            /*
            while (!ShutdownRequest.IsCancellationRequested)
            {
                await Task.Delay(2000);
            }
            await Client.UpdateStatusAsync(activity: null, userStatus: UserStatus.Offline, idleSince: null);
            await Client.DisconnectAsync();
            await Task.Delay(2500);
            Dispose();*/
        }


        #region Register Commands & Events
        /// <summary>
        /// Registers the event listener.
        /// </summary>
        /// <param name="client">The discord client.</param>
        /// <param name="ac">The application commands extension.</param>
        /// <param name="cnext">The commands next extension.</param>
        private void RegisterEventListener(DiscordClient client, ApplicationCommandsExtension ac, CommandsNextExtension cnext)
        {

            client.Ready += Client_Ready;
            client.Resumed += Client_Resumed;
            client.SocketOpened += Client_SocketOpened;
            client.SocketClosed += Client_SocketClosed;
            client.SocketErrored += Client_SocketErrored;
            client.Heartbeated += Client_Heartbeated;
            client.GuildUnavailable += Client_GuildUnavailable;
            client.GuildAvailable += Client_GuildAvailable;
            client.GuildCreated += Client_GuildCreated;
            client.GuildDeleted += Client_GuildDeleted;
            client.ApplicationCommandCreated += Discord_ApplicationCommandCreated;
            client.ApplicationCommandDeleted += Discord_ApplicationCommandDeleted;
            client.ApplicationCommandUpdated += Discord_ApplicationCommandUpdated;
            client.ComponentInteractionCreated += Client_ComponentInteractionCreated;
            client.ApplicationCommandPermissionsUpdated += Client_ApplicationCommandPermissionsUpdated;

#if DEBUG
            client.UnknownEvent += Client_UnknownEvent;
#endif
            ac.SlashCommandErrored += Ac_SlashCommandErrored;
            ac.SlashCommandExecuted += Ac_SlashCommandExecuted;
            ac.ContextMenuErrored += Ac_ContextMenuErrored;
            ac.ContextMenuExecuted += Ac_ContextMenuExecuted;
            cnext.CommandErrored += CNext_CommandErrored;
        }

        /// <summary>
        /// Registers the commands.
        /// </summary>
        /// <param name="cnext">The commands next extension.</param>
        /// <param name="ac">The application commands extensions.</param>
        private static void RegisterCommands(CommandsNextExtension cnext, ApplicationCommandsExtension ac)
        {
            cnext.RegisterCommands<DiscordCommands.Main>();
#if DEBUG
            ac.RegisterCommands<DiscordCommands.Slash>(testguild, perms =>
            {
                perms.AddRole(889266812267663380, true);
            });
#else
            ac.RegisterCommands<DiscordCommands.Slash>();
#endif
        }
        #endregion
        /*

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
            embedBuilder.WithAuthor($"ListforgeNotify {obj.Name}");
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
                            embedBuilder.Color = Color.DarkPurple;
                            embedBuilder.AddField("Player count changed to ", $"{obj.Players}/{obj.Maxplayers}");
                        }
                        else if (whatchanged == "status")
                        {
                            embedBuilder.Title = "Status changed!";
                            string sonoff = "Offline";
                            embedBuilder.Color = Color.Red;
                            if (obj.Is_online)
                            {
                                sonoff = "Online";
                                embedBuilder.Color = Color.Green;
                            }
                            embedBuilder.AddField("Status changed to ", $"{sonoff}");
                        }
                        else if(whatchanged == "version")
                        {
                            embedBuilder.Title = "Version changed!";
                            embedBuilder.Color = Color.DarkMagenta;
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
        }*/
    }
}
