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

        public HelpCommand(string servers, string statisticsservers)
        {
            _message = GenerateMessage(servers, statisticsservers);
        }

        private Embed GenerateMessage(string servers, string statisticsservers)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Title = "/help",
                Description = "This is the commandhelp for the ListforgeNotify Bot",
                Color = new Color(245, 107, 0)
            };
            embedBuilder.AddField("/42", "Show every server with less info");
            embedBuilder.AddField("/42s", "Show player statistics for every server");
            embedBuilder.AddField("/42big", "Show every server");
            embedBuilder.AddField("/list", "Show server list");
            embedBuilder.AddField($"{servers}", "Server information from single server");
            embedBuilder.AddField($"{statisticsservers}", "Player statistics from single server");
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
