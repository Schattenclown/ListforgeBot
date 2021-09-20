using DisCatSharp;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using DisCatSharp.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using static BotDLL.Model.BotCom.DiscordBot;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    /// <summary>
    /// The slash commands.
    /// </summary>
    internal class Slash : ApplicationCommandsModule
    {
        /// <summary>
        /// This is the Database Table name.
        /// </summary>
        private const string db = "LF_ServerInfoLive";

        /// <summary>
        /// Send the help of this bot.
        /// </summary>
        /// <param name="ic">The interaction context.</param>
        [SlashCommand("help", "ListforgeNotify Help", true)]
        public static async Task HelpAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder()
            {
                Title = "Help",
                Description = "This is the command help for the ListforgeNotify Bot",
                Color = new DiscordColor(245, 107, 0)
            };
            eb.AddField("/42", "Show´s every Server with their informations");
            eb.AddField("/42s", "Show´s every servers player statistics");
            eb.AddField("/42big", "Show´s every Server with a little more informations");
            eb.AddField("/list", "Show´s the server list");
            eb.AddField("/statlist", "Show server list for the player statistics");
            eb.AddField("/add", "Adds you to an subscription for a server");
            eb.AddField("/del", "About what server do you wont get notified anymore");
            eb.AddField("/abo", "Show´s about what servers you will get notified");
            eb.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            eb.WithAuthor("ListforgeNotify help");
            eb.WithFooter("(✿◠‿◠) thanks for using me");
            eb.WithTimestamp(DateTime.Now);

            await ic.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Show´s every Server with their informations
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("42", "Show´s every Server with their informations", true)]
        public static async Task ShowAllAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("loading servers"));

            _ = new List<LF_ServerInfo>();
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                DiscordEmbedBuilder eb = new DiscordEmbedBuilder();
                string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                eb.Title = item.Name;

                if (item.Name != "TheForest")
                    eb.Url = item.LF_Uri.AbsoluteUri;

                eb.AddField("Ip address", $"{item.Address}:{item.Port}", true);
                eb.AddField("Status", $"{onoff}", false);
                eb.AddField("Players", $"{item.Players}/{item.Maxplayers}", true);
                eb.WithTimestamp(item.Last_check);

                if (item.LF_HeaderImgURi != null)
                    eb.ImageUrl = item.LF_HeaderImgURi.AbsoluteUri;

                eb.Color = DiscordColor.Green;
                if (onoff == "Offline")
                    eb.Color = DiscordColor.Red;

                await ic.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(eb.Build()));
            }

            await ic.DeleteResponseAsync();
        }

        /// <summary>
        /// Show´s every Server with their informations
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("42s", "Show´s every servers player statistics", true)]
        public static async Task ShowStatsAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("loading servers"));

            _ = new List<LF_ServerInfo>();
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder();

            if (lstlive != null)
            {
                foreach (var item in lstlive)
                {
                    string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    eb.Title = item.Name;
                    if (item.Name != "TheForest")
                        eb.Url = item.LF_Uri.AbsoluteUri;

                    eb.Description = $"Player Statistics for {item.Name}";
                    if (item.QC_StatUri != null)
                        eb.ImageUrl = item.QC_StatUri.AbsoluteUri;
                    else
                        eb.Description = "Hm Broke no stats!";
                    eb.WithTimestamp(item.Last_check);

                    eb.Color = DiscordColor.Green;
                    if (onoff == "Offline")
                        eb.Color = DiscordColor.Red;

                    await ic.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(eb.Build()));
                }
            }
            /*
                else if (lF_ServerInfo != null)
                {
                    string onoff = "Offline"; if (lF_ServerInfo.Is_online == true) onoff = "Online";

                    eb.Title = lF_ServerInfo.Name;
                    if (lF_ServerInfo.Name != "TheForest")
                        eb.Url = lF_ServerInfo.LF_Uri.AbsoluteUri;

                    eb.Description = $"Player Statistics for {lF_ServerInfo.Name}";
                    eb.ImageUrl = lF_ServerInfo.QC_StatUri.AbsoluteUri;
                    eb.WithTimestamp(lF_ServerInfo.Last_check);

                    eb.Color = DiscordColor.Green;
                    if (onoff == "Offline")
                        eb.Color = DiscordColor.Red;

                    await ic.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(eb.Build()));
                }
            */

            await ic.DeleteResponseAsync();
        }

        /// <summary>
        /// Show´s every Server with a little more informations
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("42big", "Show´s every Server with a little more informations", true)]
        public static async Task ShowAllBigAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("loading servers"));

            _ = new List<LF_ServerInfo>();
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                DiscordEmbedBuilder eb = new DiscordEmbedBuilder();
                string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                eb.Title = item.Name;

                if (item.Name != "TheForest")
                    eb.Url = item.LF_Uri.AbsoluteUri;

                eb.AddField("Ip address", $"{item.Address}:{item.Port}", true);
                eb.AddField("Status", $"{onoff}", false);
                eb.AddField("Players", $"{item.Players}/{item.Maxplayers}", true);
                eb.AddField("Version", $"{item.Version}", true);
                eb.AddField("Uptime", $"{item.Uptime}%", true);
                eb.WithTimestamp(item.Last_check);

                if (item.LF_HeaderImgURi != null)
                    eb.ImageUrl = item.LF_HeaderImgURi.AbsoluteUri;

                eb.Color = DiscordColor.Green;
                if (onoff == "Offline")
                    eb.Color = DiscordColor.Red;

                await ic.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(eb.Build()));
            }

            await ic.DeleteResponseAsync();
        }

        /// <summary>
        /// Show´s the server list
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("list", "Show´s the server list", true)]
        public static async Task ListAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            _ = new List<LF_ServerInfo>();
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
            string servers = "";

            foreach (var item in lstlive)
            {
                servers += item.Name.ToLower() + "\n";
            }

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Description = "This is the list for all registered servers",
                Color = new DiscordColor(245, 107, 0)
            };
            eb.AddField($"{servers}", "Server information from single server");
            eb.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            eb.WithAuthor("ListforgeNotify list");
            eb.WithFooter("(✿◠‿◠) thanks for using me");
            eb.WithTimestamp(DateTime.Now);

            await ic.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Checks the exp async.
        /// </summary>
        /// <param name="ctx">The ctx.</param>
        /// <param name="experiment">The experiment.</param>
        /// <param name="gid">The gid.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("add", "Adds you to an subscription for a server")]
        public static async Task CheckExpAsync(InteractionContext ctx, [ChoiceProvider(typeof(ExperimentChoiceProvider))][Option("Server", "adding")] string servers)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Loading..." ));

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder();

            var ecp = new ExperimentChoiceProvider();
            var p = await ecp.Provider();
            switch (servers)
            {
                case "0":                   

                    eb = ChangeSubscriptionCommand(p.First(d => d.Value.ToString() == servers).Name, ctx);
                    break;
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    return;
                case "4":
                    return;
                default:
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Invalid Experiment" ));
                    return;
            }

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Subadd command
        /// </summary>
        /// <param name="servername">The servername.</param>
        /// <param name="ctx">The ctx.</param>
        /// <returns>A DiscordEmbedBuilder.</returns>
        public static DiscordEmbedBuilder ChangeSubscriptionCommand(string servername, InteractionContext ctx)
        {
            bool found = false;
            int serverid = 0;
            var lstud = DB_DC_Userdata.ReadAll();
            var lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                if (item.Name == servername)
                    serverid = item.Id;
            }

            foreach (var item in lstud)
            {
                if (item.ChannelId == ctx.Channel.Id.ToString() && item.ServerId == serverid && item.AuthorId == ctx.Member.Id.ToString())
                    found = true;
            }

            if (!found)
            {
                DC_Userdata ud = new DC_Userdata
                {
                    AuthorId = ctx.Member.Id.ToString(),
                    ChannelId = ctx.Channel.Id.ToString(),
                    Author = ctx.Member.DisplayName,
                    ServerId = serverid,
                    Abo = true
                };
                DC_Userdata.Add(ud, true);
            }
            else if (found)
            {
                DC_Userdata ud = new DC_Userdata
                {
                    AuthorId = ctx.Member.Id.ToString(),
                    ChannelId = ctx.Channel.Id.ToString(),
                    Author = ctx.Member.DisplayName,
                    ServerId = serverid,
                    Abo = true
                };
                DC_Userdata.Change(ud, true);
            }

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = $"You will get notifications for {servername}!",
                Color = new DiscordColor(245, 107, 0)
            };
            eb.WithDescription("Who?:" + "<@" + ctx.Member.Id.ToString() + ">\n" + "Where?:" + "<#" + ctx.Channel.Id.ToString() + ">");
            eb.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            eb.WithAuthor("ListforgeNotify abo");
            eb.WithFooter("(✿◠‿◠) thanks for using me");
            eb.WithTimestamp(DateTime.Now);
            eb.Build();

            return eb;
        }

        /// <summary>
        /// The experiment choice provider.
        /// </summary>
        //[ChoiceProvider(typeof(ExperimentChoiceProvider))][Option("experiment", "Experiment")] string experiment
        public class ExperimentChoiceProvider : IChoiceProvider
        {
            public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
            {
                _ = new List<LF_ServerInfo>();
                List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
                string servers = "";

                foreach (var item in lstlive)
                {
                    servers += item.Name.ToLower() + ";";
                }
                servers = servers.Trim(';');
                string[] arrServers = servers.Split(';');

                DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[arrServers.Length];

                int i = 0;

                foreach (var item in arrServers)
                {
                    choices[i] = new DiscordApplicationCommandOptionChoice(item, i.ToString());
                    i++;
                }

                return choices;
            }
        }

        /// <summary>
        /// Show´s about what servers you will get notified
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("abo", "Show´s about what servers you will get notified", true)]
        public static async Task ShowAboAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            var lstud = DB_DC_Userdata.ReadAll();
            var lstlive = LF_ServerInfo.ReadAll(db);
            bool sub2nothing = true;
            var differentchannel = new List<ulong>();
            string servers = "";
            var lstDC_Abo = new List<DC_Abo>();
            var lstDC_Abo_sort = new List<DC_Abo>();

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = "/abo",
                Color = new DiscordColor(245, 107, 0)
            };
            eb.WithDescription(ic.Member.Mention);
            eb.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            eb.WithAuthor("ListforgeNotify abo");
            eb.WithFooter("(✿◠‿◠) thanks for using me");
            eb.WithTimestamp(DateTime.Now);

            foreach (var ld in lstlive)
            {
                foreach (var ud in lstud)
                {
                    if (ud.Abo && ud.ServerId == ld.Id && Convert.ToUInt64(ud.AuthorId) == ic.Member.Id)
                    {
                        DC_Abo dcabo = new DC_Abo(Convert.ToUInt64(ud.ChannelId), Convert.ToUInt64(ud.AuthorId), ld.Name);

                        if (!differentchannel.Contains(Convert.ToUInt64(ud.ChannelId)) && Convert.ToUInt64(ud.AuthorId) == ic.Member.Id)
                            differentchannel.Add(Convert.ToUInt64(ud.ChannelId));

                        lstDC_Abo.Add(dcabo);

                        sub2nothing = false;
                    }
                }
            }

            if (sub2nothing)
                eb.AddField("You are unsubscribed to everything", ":(");
            else
            {
                foreach (var diffchn in differentchannel)
                {
                    foreach (var dC_Abo in lstDC_Abo)
                    {

                        if (!lstDC_Abo_sort.Contains(dC_Abo) && dC_Abo.ChannelId == diffchn)
                        {
                            lstDC_Abo_sort.Add(dC_Abo);
                        }
                    }
                }
            }

            ulong lastchn = 0;

            foreach (var item in lstDC_Abo_sort)
            {
                if (lastchn == 0)
                    lastchn = item.ChannelId;

                if (!servers.Contains(item.Server))
                    servers += "/" + item.Server + "\n";

                if (lastchn != item.ChannelId)
                {
                    eb.AddField(servers, "<#" + lastchn.ToString() + ">");
                    servers = "";
                    servers += "/" + item.Server + "\n";
                    lastchn = item.ChannelId;
                }

                if (lstDC_Abo_sort.Last() == item)
                {
                    eb.AddField(servers, "<#" + lastchn.ToString() + ">");
                }
            }

            await ic.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Testst the functionality of the DCChange [player, status, version]
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("test", "Testst the functionality of the DCChange [player, status, version]", true)]
        public static async Task TestAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            _ = new List<LF_ServerInfo>();
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
            LF_ServerInfo obj = lstlive[1];
            DiscordBot.DCChange(obj, "player");
            DiscordBot.DCChange(obj, "status");
            DiscordBot.DCChange(obj, "version");
        }

        /// <summary>
        /// Gets the user's avatar & banner.
        /// </summary>
        /// <param name="ctx">The contextmenu context.</param>
        [ContextMenu(ApplicationCommandType.User, "Get avatar & banner")]
        public static async Task GetUserBannerAsync(ContextMenuContext ctx)
        {
            var user = await ctx.Client.GetUserAsync(ctx.TargetUser.Id, true);

            var eb = new DiscordEmbedBuilder
            {
                Title = $"Avatar & Banner of {user.Username}",
                ImageUrl = user.BannerHash != null ? user.BannerUrl : null
            }.
            WithThumbnail(user.AvatarUrl).
            WithColor(user.BannerColor ?? DiscordColor.Aquamarine).
            WithFooter($"Requested by {ctx.Member.DisplayName}", ctx.Member.AvatarUrl).
            WithAuthor($"{user.Username}", user.AvatarUrl, user.AvatarUrl);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Generates an Invite link.
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("invite", "Invite ListforgeNotify", true)]
        public static async Task InviteAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var bot_invite = ic.Client.GetInAppOAuth(Permissions.Administrator);

            await ic.EditResponseAsync(new DiscordWebhookBuilder().WithContent(bot_invite.AbsoluteUri));
        }
    }

    /*
            _ = new List<LF_ServerInfo>();
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
            string servers;

            foreach (var item in lstlive)
            {
                servers += "/" + item.Name.ToLower() + "\n";
                statisticsservers += "/s" + item.Name.ToLower() + "\n";
                addme += "/add" + item.Name + "\n";
                delme += "/del" + item.Name + "\n";
            }
    */
}
