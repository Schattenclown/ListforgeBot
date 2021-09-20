﻿using BotDLL.Model.BotCom.DiscordCommands.Base;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    public class ShowOneCommand : IDiscordCommandAsync
    {
        private readonly SocketMessage arg;
        private readonly List<LF_ServerInfo> serverInfos;

        private Embed _message;

        public ShowOneCommand(SocketMessage arg, List<LF_ServerInfo> serverInfos)
        {
            _message = null;

            this.arg = arg;
            this.serverInfos = serverInfos;
        }

        public async Task<bool> Execute()
        {
            if (!arg.Author.IsBot)
            {
                foreach (var item in serverInfos)
                {
                    EmbedBuilder embedBuilder = new EmbedBuilder();

                    String onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    if (item.Name.ToLower() == arg.Content.ToLower().Trim('/'))
                    {
                        embedBuilder.Title = item.Name;

                        if (item.Name != "TheForest")
                            embedBuilder.Url = item.LF_Uri.AbsoluteUri;

                        embedBuilder.AddField("Ip address", $"{item.Address}:{item.Port}", true);
                        embedBuilder.AddField("Status", $"{onoff}", false);
                        embedBuilder.AddField("Players", $"{item.Players}/{item.Maxplayers}", true);
                        embedBuilder.AddField("Version", $"{item.Version}", true);
                        embedBuilder.AddField("Uptime", $"{item.Uptime}", true);
                        embedBuilder.WithTimestamp(item.Last_check);
                        embedBuilder.Color = Color.Green;

                        if (onoff == "Offline")
                        {
                            embedBuilder.Color = Color.Red;
                            embedBuilder.Description += $"Last online:  { item.Last_online}";
                        }

                        if (item.LF_Uri.AbsoluteUri != null)
                            embedBuilder.ImageUrl = item.LF_HeaderImgURi.AbsoluteUri;

                        await arg.Channel.SendMessageAsync(null, false, embedBuilder.Build());
                    }
                    else if ('s' + item.Name.ToLower() == arg.Content.ToLower().Trim('/'))
                    {
                        embedBuilder.Title = item.Name;
                        embedBuilder.Description = $"Player Statistics for {item.Name}";
                        embedBuilder.ImageUrl = item.QC_StatUri.AbsoluteUri;

                        if (item.Name != "TheForest")
                            embedBuilder.Url = item.LF_Uri.AbsoluteUri;

                        embedBuilder.WithTimestamp(item.Last_check);
                        embedBuilder.Color = Color.Green;

                        if (onoff == "Offline")
                            embedBuilder.Color = Color.Red;

                        await arg.Channel.SendMessageAsync(null, false, embedBuilder.Build());
                    }
                }

                return true;
            }

            return false;
        }

        public Task<Embed> GetMessage()
        {
            return Task.FromResult(_message);
        }
    }
}