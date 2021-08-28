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
    public class ShowAllCommand : IDiscordCommandAsync
    {
        private readonly SocketMessage arg;
        private readonly List<LF_ServerInfo> serverInfos;

        private Embed _message;

        public ShowAllCommand(SocketMessage arg, List<LF_ServerInfo> serverInfos)
        {
            _message = null;

            this.arg = arg;
            this.serverInfos = serverInfos;
        }

        public async Task<bool> Execute()
        {
            if (!arg.Author.IsBot)
            {
                EmbedBuilder embedBuilder = new EmbedBuilder();

                foreach (var item in serverInfos)
                {
                    string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    embedBuilder.Title = item.Name;

                    if (item.Name != "TheForest")
                        embedBuilder.Url = item.LF_Uri.AbsoluteUri;

                    embedBuilder.Description = $"IP:           {item.Address}:{item.Port}\n" +
                                        $"Status:       {onoff}\n" +
                                        $"Players:      {item.Players}/{item.Maxplayers}\n" +
                                        $"Version:      {item.Version}\n" +
                                        $"Uptime:       {item.Uptime}%\n" +
                                        $"Last check:   {item.Last_check}\n";

                    if (item.LF_Uri.AbsoluteUri != null)
                        embedBuilder.ImageUrl = item.LF_HeaderImgURi.AbsoluteUri;

                    embedBuilder.Color = Color.Green;

                    if (onoff == "Offline")
                        embedBuilder.Color = Color.Red;

                    await arg.Channel.SendMessageAsync(null, false, embedBuilder.Build());
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
