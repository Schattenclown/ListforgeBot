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

    /// <summary>
    /// The discord bot.
    /// </summary>
    public class DiscordBot : IDisposable
    {   /* 
              möpse
            ( . )( . )
             )      (
            (   oo   )
             |  ||  | 
             |  V   |
        */

        private static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        static List<DC_Userdata> lstud = new List<DC_Userdata>();
        /// <summary>
        /// The db.
        /// </summary>
        public const string db = "LF_ServerInfoLive";
        public static string token = "";
        public static int virgin = 0;
        /// <summary>
        /// Gets the client.
        /// </summary>
        public static DiscordClient Client { get; internal set; }
        /// <summary>
        /// Gets the application commands extension.
        /// </summary>
        public static ApplicationCommandsExtension ApplicationCommands { get; internal set; }
        /// <summary>
        /// Gets the commands next extension.
        /// </summary>
        public static CommandsNextExtension CNext { get; internal set; }
        /// <summary>
        /// Gets the interactivity extension.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordBot"/> class.
        /// </summary>
        public DiscordBot()
        {
            if (virgin == 0)
            {
                Connections connections = Connections.GetConnections();
                token = connections.DiscordBotKey;
#if DEBUG
                token = connections.DiscordBotDebug;
#endif
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
        /// <summary>
        /// Disposes the bot.
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
            INext = null;
            CNext = null;
            Client = null;
            ApplicationCommands = null;
        }

        /// <summary>
        /// Runs the bot.
        /// </summary>
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
        
        public static async void DCChange(LF_ServerInfo obj, string whatchanged)
        {
            lstud = DC_Userdata.ReadAll();
            var differentchannel = new List<ulong>();
            bool once = false;

            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder
            {
                Color = new DiscordColor(245, 107, 0)
            };
            embedBuilder.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            embedBuilder.WithAuthor($"ListforgeNotify");
            embedBuilder.WithFooter("(✿◠‿◠) thanks for using me");
            embedBuilder.WithTimestamp(DateTime.Now);

            foreach (var item in lstud)
            {
                if (item.Abo && item.ServerId == obj.Id)
                {
                    if(!once)
                    {
                        embedBuilder.Url = obj.LF_Uri.AbsoluteUri;
                        embedBuilder.AddField($"Name", $"{obj.Name}");
                        embedBuilder.AddField("Ip address", $"{obj.Address}:{obj.Port}");
                        if( whatchanged == "player")
                        {
                            embedBuilder.WithAuthor($"ListforgeNotify Player count changed!");
                            embedBuilder.Title = $"{obj.Name}";
                            embedBuilder.Color = DiscordColor.Gold;
                            embedBuilder.AddField("Player count changed to ", $"{obj.Players}/{obj.Maxplayers}");
                        }
                        else if (whatchanged == "status")
                        {
                            embedBuilder.WithAuthor($"ListforgeNotify Status changed!");
                            embedBuilder.Title = $"{obj.Name}";
                            string sonoff = "Offline";
                            embedBuilder.Color = DiscordColor.Red;
                            if (obj.Is_online)
                            {
                                sonoff = "Online";
                                embedBuilder.Color = DiscordColor.Green;
                            }
                            embedBuilder.AddField("Status changed to ", $"{sonoff}");
                        }
                        else if(whatchanged == "version")
                        {
                            embedBuilder.WithAuthor($"ListforgeNotify Version changed!");
                            embedBuilder.Title = $"{obj.Name}";
                            embedBuilder.Color = DiscordColor.Gray;
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
                    if (!tags.Contains(item2.AuthorId.ToString()) && item == Convert.ToUInt64(item2.ChannelId) && item2.Abo && !item2.MinimalAbo && item2.ServerId == obj.Id && !tags.Contains(item2.AuthorId.ToString()))
                        tags += "<@" + item2.AuthorId + ">" + "\n";
                    else if (!tags.Contains(item2.AuthorId.ToString()) && item == Convert.ToUInt64(item2.ChannelId) && item2.MinimalAbo && item2.ServerId == obj.Id && obj.Players == 0 ||
                             !tags.Contains(item2.AuthorId.ToString()) && item == Convert.ToUInt64(item2.ChannelId) && item2.MinimalAbo && item2.ServerId == obj.Id && obj.Players == 1 )
                        tags += "<@" + item2.AuthorId + ">" + "\n";
                    else if(!tags.Contains(item2.AuthorId.ToString()) && item == Convert.ToUInt64(item2.ChannelId) && item2.Abo && item2.MinimalAbo && item2.ServerId == obj.Id && whatchanged == "version" ||
                            !tags.Contains(item2.AuthorId.ToString()) && item == Convert.ToUInt64(item2.ChannelId) && item2.Abo && item2.MinimalAbo && item2.ServerId == obj.Id && whatchanged == "status" )
                        tags += "<@" + item2.AuthorId + ">" + "\n";
                }
                
                embedBuilder.WithDescription(tags);

                var chn = await Client.GetChannelAsync(item);
            
                if(chn != null && tags != "")
                    await chn.SendMessageAsync(embedBuilder.Build());

                tags = "";
            }
        }

        /// <summary>
        /// Centers the text.
        /// </summary>
        /// <param name="s">The string.</param>
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

        /// <summary>
        /// Logs a string.
        /// </summary>
        /// <param name="msg">The message.</param>
        private static Task Log(string msg)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                Center($"DISCORD: {msg}");
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
