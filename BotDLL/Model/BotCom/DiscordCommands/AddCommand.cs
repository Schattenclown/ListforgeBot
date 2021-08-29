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
    public class AddCommand : IDiscordCommandAsync
    {
        private readonly SocketMessage arg;

        private Embed _message;

        public AddCommand(string addme)
        {
            _message = GenerateMessage(addme);
        }

        private Embed GenerateMessage(string addme)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Title = "/add",
                Description = "This is the notification abo system for the ListforgeNotify Bot",
                Color = new Color(245, 107, 0)
            };
            embedBuilder.AddField($"{addme}", "About what server do you want to get notified");
            embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
            embedBuilder.WithAuthor("ListforgeNotify add");
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
