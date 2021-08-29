using BotDLL.Model.BotCom.DiscordCommands.Base;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    public class HelpCommand : IDiscordCommandAsync
    {
        private Embed _message;

        public HelpCommand()
        {
            _message = GenerateMessage();
        }

        private Embed GenerateMessage()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Title = "/help",
                Description = "This is the commandhelp for the ListforgeNotify Bot",
                Color = new Color(245, 107, 0)
            };
            embedBuilder.AddField("/42", "Show every server with there stats");
            embedBuilder.AddField("/42s", "Show every servers player statistics");
            embedBuilder.AddField("/42big", "Show every server with a little more stats");
            embedBuilder.AddField("/list", "Show server list");
            embedBuilder.AddField("/statlist", "Show server list for the player statistics");
            embedBuilder.AddField("/add", "About what server do you want to get notified");
            embedBuilder.AddField("/del", "About what server do you wont get notified anymore");
            embedBuilder.AddField("/abo", "Show about what servers you will get notified");
            embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
            embedBuilder.WithAuthor("ListforgeNotify help");
            embedBuilder.WithFooter("(✿◠‿◠) thanks for using me");
            embedBuilder.WithTimestamp(DateTime.Now);

            return embedBuilder.Build();
        }

        public Task<bool> Execute()
        {
            return Task.FromResult(true);
        }

        public Task<Embed> GetMessage()
        {
            return Task.FromResult(_message);
        }
    }
}
