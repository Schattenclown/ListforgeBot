﻿using BotDLL.Model.BotCom.DiscordCommands.Base;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    public class ListCommand : IDiscordCommandAsync
    {
        private Embed _message;

        public ListCommand(string servers)
        {
            _message = GenerateMessage(servers);
        }

        private Embed GenerateMessage(string servers)
        {

            EmbedBuilder embedBuilder = new EmbedBuilder
            {
                Description = "This is the list for all registered servers",
                Color = new Color(245, 107, 0)
            };
            embedBuilder.AddField($"{servers}", "Server information from single server");
            embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
            embedBuilder.WithAuthor("ListforgeNotify serverlist");
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