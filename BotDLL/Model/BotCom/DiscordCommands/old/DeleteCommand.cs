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
    public class DeleteCommand : IDiscordCommandAsync
    {
        private Embed _message;

        public DeleteCommand(string delme)
        {
            _message = GenerateMessage(delme);
        }

        private Embed GenerateMessage(string delme)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Title = "/del",
                Description = "This is the notification abo system for the ListforgeNotify Bot",
                Color = new Color(245, 107, 0)
            };
            embedBuilder.AddField($"{delme}", "About what server do you want to not get notified");
            embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
            embedBuilder.WithAuthor("ListforgeNotify del");
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