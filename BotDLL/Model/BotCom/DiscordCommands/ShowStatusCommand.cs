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
    public class ShowStatusCommand : IDiscordCommandAsync
    {
        private readonly SocketMessage arg;
        private readonly List<LF_ServerInfo> serverInfos;

        private Embed _message;

        public ShowStatusCommand(SocketMessage arg, List<LF_ServerInfo> serverInfos)
        {
            _message = null;

            this.arg = arg;
            this.serverInfos = serverInfos;
        }

        public async Task<bool> Execute()
        {
            if (!arg.Author.IsBot)
            {
                var eb = new EmbedBuilder();

                foreach (var item in serverInfos)
                {
                    string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                    eb.Title = item.Name;
                    if (item.Name != "TheForest")
                        eb.Url = item.LF_Uri.AbsoluteUri;

                    eb.Description = $"Player Statistics for {item.Name}";
                    eb.ImageUrl = item.QC_StatUri.AbsoluteUri;

                    eb.Color = Color.Green;
                    if (onoff == "Offline")
                        eb.Color = Color.Red;

                    await arg.Channel.SendMessageAsync(null, false, eb.Build());
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
