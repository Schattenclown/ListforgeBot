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
    public class ShowStatsCommand : IDiscordCommandAsync
    {
        private readonly SocketMessage arg;
        private readonly List<LF_ServerInfo> serverInfos;
        private readonly LF_ServerInfo lF_ServerInfo;

        private Embed _message;

        public ShowStatsCommand(SocketMessage arg, List<LF_ServerInfo> serverInfos)
        {
            _message = null;

            this.arg = arg;
            this.serverInfos = serverInfos;
        }
        public ShowStatsCommand(SocketMessage arg, LF_ServerInfo lF_ServerInfo)
        {
            _message = null;

            this.arg = arg;
            this.lF_ServerInfo = lF_ServerInfo;
        }

        public async Task<bool> Execute()
        {
            if (!arg.Author.IsBot)
            {
                var eb = new EmbedBuilder();

                if(serverInfos != null)
                {
                    foreach (var item in serverInfos)
                    {
                        string onoff = "Offline"; if (item.Is_online == true) onoff = "Online";

                        eb.Title = item.Name;
                        if (item.Name != "TheForest")
                            eb.Url = item.LF_Uri.AbsoluteUri;

                        eb.Description = $"Player Statistics for {item.Name}";
                        eb.ImageUrl = item.QC_StatUri.AbsoluteUri;
                        eb.WithTimestamp(item.Last_check);

                        eb.Color = Color.Green;
                        if (onoff == "Offline")
                            eb.Color = Color.Red;

                        await arg.Channel.SendMessageAsync(null, false, eb.Build());
                    }
                }
                else if (lF_ServerInfo != null)
                {
                    string onoff = "Offline"; if (lF_ServerInfo.Is_online == true) onoff = "Online";

                    eb.Title = lF_ServerInfo.Name;
                    if (lF_ServerInfo.Name != "TheForest")
                        eb.Url = lF_ServerInfo.LF_Uri.AbsoluteUri;

                    eb.Description = $"Player Statistics for {lF_ServerInfo.Name}";
                    eb.ImageUrl = lF_ServerInfo.QC_StatUri.AbsoluteUri;
                    eb.WithTimestamp(lF_ServerInfo.Last_check);

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
