using DisCatSharp;
using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using DisCatSharp.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
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

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder()
            {

                Title = "Help",
                Description = "This is the command help for the ListforgeNotify Bot",
                Color = new DiscordColor(245, 107, 0)
            };
            builder.AddField("/42", "Show´s every Server with their informations");
            builder.AddField("/42s", "Show every servers player statistics");
            builder.AddField("/42big", "Show every server with a little more stats");
            builder.AddField("/list", "Show server list");
            builder.AddField("/statlist", "Show server list for the player statistics");
            builder.AddField("/add", "About what server do you want to get notified");
            builder.AddField("/del", "About what server do you wont get notified anymore");
            builder.AddField("/abo", "Show about what servers you will get notified");
            builder.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            builder.WithAuthor("ListforgeNotify help");
            builder.WithFooter("(✿◠‿◠) thanks for using me");
            builder.WithTimestamp(DateTime.Now);

            await ic.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(builder.Build()));
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
                DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();
                string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                embedBuilder.Title = item.Name;

                if (item.Name != "TheForest")
                    embedBuilder.Url = item.LF_Uri.AbsoluteUri;

                embedBuilder.AddField("Ip address", $"{item.Address}:{item.Port}", true);
                embedBuilder.AddField("Status", $"{onoff}", false);
                embedBuilder.AddField("Players", $"{item.Players}/{item.Maxplayers}", true);
                embedBuilder.AddField("Version", $"{item.Version}", true);
                embedBuilder.AddField("Uptime", $"{item.Uptime}", true);
                embedBuilder.WithTimestamp(item.Last_check);

                if (item.LF_HeaderImgURi != null)
                    embedBuilder.ImageUrl = item.LF_HeaderImgURi.AbsoluteUri;

                embedBuilder.Color = DiscordColor.Green;

                if (onoff == "Offline")
                    embedBuilder.Color = DiscordColor.Red;

                await ic.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embedBuilder.Build()));
                Task.Delay(1000);
            }

            await ic.DeleteResponseAsync();
        }

        /// <summary>
        /// The experiment choice provider.
        /// </summary>
        //[ChoiceProvider(typeof(ExperimentChoiceProvider))][Option("experiment", "Experiment")] string experiment
        public class ExperimentChoiceProvider : IChoiceProvider
        {
            public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
            {
                return new DiscordApplicationCommandOptionChoice[]
                {
                    new DiscordApplicationCommandOptionChoice("Role Icons", "1"),
                    new DiscordApplicationCommandOptionChoice("Activities", "2"),
                    new DiscordApplicationCommandOptionChoice("Stage Events", "3"),
                    new DiscordApplicationCommandOptionChoice("Role Subscriptions", "4")
                };
            }
        }

        /// <summary>
        /// Shows the abo async.
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("abo", "Show about what servers you will get notified", true)]
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

            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder
            {
                Title = "/abo",
                Color = new DiscordColor(245, 107, 0)
            };
            embedBuilder.WithDescription(ic.Member.Mention);
            embedBuilder.WithThumbnail("https://i.imgur.com/eb0RgjI.png");
            embedBuilder.WithAuthor("ListforgeNotify abo");
            embedBuilder.WithFooter("(✿◠‿◠) thanks for using me");
            embedBuilder.WithTimestamp(DateTime.Now);

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
                embedBuilder.AddField("You are unsubscribed to everything", ":(");
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
                    embedBuilder.AddField(servers, "<#" + lastchn.ToString() + ">");
                    servers = "";
                    servers += "/" + item.Server + "\n";
                    lastchn = item.ChannelId;
                }

                if (lstDC_Abo_sort.Last() == item)
                {
                    embedBuilder.AddField(servers, "<#" + lastchn.ToString() + ">");
                }
            }

            await ic.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedBuilder.Build()));
        }

        /// <summary>
        /// Gets the user's avatar & banner.
        /// </summary>
        /// <param name="ctx">The contextmenu context.</param>
        [ContextMenu(ApplicationCommandType.User, "Get avatar & banner")]
        public static async Task GetUserBannerAsync(ContextMenuContext ctx)
        {
            var user = await ctx.Client.GetUserAsync(ctx.TargetUser.Id, true);

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Avatar & Banner of {user.Username}",
                ImageUrl = user.BannerHash != null ? user.BannerUrl : null
            }.
            WithThumbnail(user.AvatarUrl).
            WithColor(user.BannerColor ?? DiscordColor.Aquamarine).
            WithFooter($"Requested by {ctx.Member.DisplayName}", ctx.Member.AvatarUrl).
            WithAuthor($"{user.Username}", user.AvatarUrl, user.AvatarUrl);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed.Build()));
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

        /// <summary>
        /// Tests the async.
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("test", "test", true)]
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
        /// Show´s every Server with their informations
        /// </summary>
        /// <param name="ic">The ic.</param>
        /// <returns>A Task.</returns>
        [SlashCommand("42s", "Show every servers player statistics", true)]
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
            /*else if (lF_ServerInfo != null)
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
                Task.Delay(1000);
            }*/

            await ic.DeleteResponseAsync();
        }
    }
}
