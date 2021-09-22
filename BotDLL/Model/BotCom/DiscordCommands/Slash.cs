﻿using DisCatSharp;
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
            eb.AddField("/status", "Show´s status from a singel server");
            eb.AddField("/statistics", "Show´s the playerstatistics from a singel server");
            eb.AddField("/add", "Adds you to an subscription for a server");
            eb.AddField("/addall", "Adds you to every serversubscription");
            eb.AddField("/del", "About what server do you wont get notified anymore");
            eb.AddField("/delall", "Deletes you from every serversubscription");
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
            
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                DiscordEmbedBuilder eb = new DiscordEmbedBuilder();
                string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                eb.Title = item.Name;
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

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder();

            if (lstlive != null)
            {
                foreach (var item in lstlive)
                {
                    string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    eb.Title = item.Name;
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

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                DiscordEmbedBuilder eb = new DiscordEmbedBuilder();
                string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                eb.Title = item.Name;
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

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
            string servers = "";

            foreach (var item in lstlive)
            {
                servers += item.Name.ToUpper() + "\n";
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
        /// Show´s status of a singel server
        /// </summary>
        /// <param name="ctx">The ctx.</param>
        /// <param name="servers">The servers.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("status", "Show´s status from a singel server")]
        public static async Task StatusAsync(InteractionContext ctx, [ChoiceProvider(typeof(ServerNameChoiceProvider))][Option("Server", "status")] string servers)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Loading..."));

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
            var ecp = new ServerNameChoiceProvider();
            var choices = await ecp.Provider();
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder();

            foreach (var item in lstlive)
            {
                if(item.Name.ToLower() == choices.First(d => d.Value.ToString().ToLower() == servers).Name.ToLower())
                {
                    string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    eb.Title = item.Name;
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
                }
            }

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Show´s the playerstatistics from a singel server
        /// </summary>
        /// <param name="ctx">The ctx.</param>
        /// <param name="servers">The servers.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("statistics", "Show´s the playerstatistics from a singel server")]
        public static async Task StatisticsAsync(InteractionContext ctx, [ChoiceProvider(typeof(ServerNameChoiceProvider))][Option("Server", "statistics")] string servers)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Loading..."));

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
            var ecp = new ServerNameChoiceProvider();
            var choices = await ecp.Provider();
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder();

            foreach (var item in lstlive)
            {
                if (item.Name.ToLower() == choices.First(d => d.Value.ToString().ToLower() == servers).Name.ToLower())
                {
                    string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    eb.Title = item.Name;
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
                }
            }

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Adds you to an subscription for a server
        /// </summary>
        /// <param name="ctx">The ctx.</param>
        /// <param name="experiment">The experiment.</param>
        /// <param name="gid">The gid.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("add", "Adds you to an subscription for a server")]
        public static async Task AddAboAsync(InteractionContext ctx, [ChoiceProvider(typeof(ServerNameChoiceProvider))][Option("Server", "adding")] string servers)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Loading..."));

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder();

            var ecp = new ServerNameChoiceProvider();
            var choices = await ecp.Provider();

            eb = ChangeSubscriptionCommand(choices.First(d => d.Value.ToString().ToLower() == servers).Name, ctx, true);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Adds you to every serversubscription
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("addall", "Adds you to every serversubscription", true)]
        public static async Task AddAllAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("loading servers"));

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                DiscordEmbedBuilder eb = ChangeSubscriptionCommand(item.Name, ic, true);

                await ic.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(eb.Build()));
            }

            await ic.DeleteResponseAsync();
        }

        /// <summary>
        /// Delete´s you from an subscription for a server
        /// </summary>
        /// <param name="ctx">The ctx.</param>
        /// <param name="experiment">The experiment.</param>
        /// <param name="gid">The gid.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("del", "Delete´s you from an subscription for a server")]
        public static async Task DelAboAsync(InteractionContext ctx, [ChoiceProvider(typeof(ServerNameChoiceProvider))][Option("Server", "deleting")] string servers)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Loading..."));

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder();

            var ecp = new ServerNameChoiceProvider();
            var choices = await ecp.Provider();

            eb = ChangeSubscriptionCommand(choices.First(d => d.Value.ToString().ToLower() == servers).Name, ctx, false);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(eb.Build()));
        }

        /// <summary>
        /// Delete´s you from an subscription for a server
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("delall", "Deletes you from every serversubscription", true)]
        public static async Task DelAllAsync(InteractionContext ic)
        {
            await ic.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("loading servers"));

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                DiscordEmbedBuilder eb = ChangeSubscriptionCommand(item.Name, ic, false);

                await ic.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(eb.Build()));
            }

            await ic.DeleteResponseAsync();
        }

        /// <summary>
        /// ChangeSubscriptionCommand
        /// </summary>
        /// <param name="servername">The servername.</param>
        /// <param name="ctx">The ctx.</param>
        /// <returns>A DiscordEmbedBuilder.</returns>
        public static DiscordEmbedBuilder ChangeSubscriptionCommand(string servername, InteractionContext ctx, bool abo)
        {
            bool found = false;
            int serverid = 0;
            var lstud = DB_DC_Userdata.ReadAll();
            var lstlive = LF_ServerInfo.ReadAll(db);

            foreach (var item in lstlive)
            {
                if (item.Name.ToLower() == servername.ToLower())
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
                    Abo = abo
                };
                DC_Userdata.Add(ud, false);
            }
            else if (found)
            {
                DC_Userdata ud = new DC_Userdata
                {
                    AuthorId = ctx.Member.Id.ToString(),
                    ChannelId = ctx.Channel.Id.ToString(),
                    Author = ctx.Member.DisplayName,
                    ServerId = serverid,
                    Abo = abo
                };
                DC_Userdata.Change(ud, false);
            }

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Color = new DiscordColor(245, 107, 0)
            };
            eb.Title = $"You will not get notified for {servername} anymore!";
            if (abo)
                eb.Title = $"You will get notifications for {servername}!";
            eb.WithDescription("Who?:" + "<@" + ctx.Member.Id.ToString() + ">\n" + "Where?:" + "<#" + ctx.Channel.Id.ToString() + ">");
            eb.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            eb.WithAuthor($"ListforgeNotify {servername.ToUpper()}");
            eb.WithFooter("(✿◠‿◠) thanks for using me");
            eb.WithTimestamp(DateTime.Now);
            eb.Build();

            return eb;
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
        public static async Task TestAsync(InteractionContext ic, [ChoiceProvider(typeof(ServerNameChoiceProvider))][Option("Server", "testserver")] string servers)
        {
            await ic.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var ecp = new ServerNameChoiceProvider();
            var choices = await ecp.Provider();

            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
           
            foreach (var item in lstlive)
            {
                if (item.Name.ToLower() == choices.First(d => d.Value.ToString().ToLower() == servers).Name.ToLower())
                {
                    DiscordBot.DCChange(item, "player");
                    DiscordBot.DCChange(item, "status");
                    DiscordBot.DCChange(item, "version");
                }
            }

            await ic.DeleteResponseAsync();
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
}
