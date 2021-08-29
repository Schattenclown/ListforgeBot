using BotDLL.Model.BotCom.DiscordCommands.Base;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    public class AboutMeCommand : IDiscordCommandAsync
    {
        private readonly SocketMessage arg;

        private Embed _message;

        public AboutMeCommand(SocketMessage arg)
        {
            _message = null;

            this.arg = arg;
        }

        public async Task<bool> Execute()
        {
            if (!arg.Author.IsBot)
            {
                EmbedBuilder embedBuilder = new EmbedBuilder
                {
                    Title = "/abo",
                    Color = new Color(245, 107, 0)
                };
                embedBuilder.AddField($"{arg.Author}", "author");
                embedBuilder.AddField($"{arg.Author.Id}", "autor.id");
                embedBuilder.AddField($"{arg.Channel}", "channel");
                embedBuilder.AddField($"{arg.Channel.Id}", "channel.id");
                embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
                embedBuilder.WithAuthor("ListforgeNotify abo");
                embedBuilder.WithFooter("(✿◠‿◠) thanks for using me");
                embedBuilder.WithTimestamp(DateTime.Now);

                await arg.Channel.SendMessageAsync(null, false, embedBuilder.Build());
            }

            return false;
        }

        public Task<Embed> GetMessage()
        {
            return Task.FromResult(_message);
        }
    }
}
