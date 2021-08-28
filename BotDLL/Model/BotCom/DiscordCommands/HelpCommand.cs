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
            embedBuilder.AddField("/42", "show every server with less info");
            embedBuilder.AddField("/42s", "show playerstatistics for every server");
            embedBuilder.AddField("/42big", "show every server");
            embedBuilder.AddField("/list", "show serverlist");
            embedBuilder.AddField($"{servers}", "Serverinfo from single server");
            embedBuilder.AddField($"{statisticsservers}", "Playerstatistics from single server");
            embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
            embedBuilder.WithAuthor("ListforgeNotify");
            embedBuilder.WithFooter("(✿◠‿◠) ty for using me");
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
