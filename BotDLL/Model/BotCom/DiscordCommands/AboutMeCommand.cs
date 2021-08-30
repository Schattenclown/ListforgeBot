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
        private readonly List<DC_Userdata> lstud;
        private readonly List<LF_ServerInfo> lstlive;
        private readonly SocketMessage arg;

        private Embed _message;

        public AboutMeCommand(List<DC_Userdata> lstud, List<LF_ServerInfo> lstlive, SocketMessage arg)
        {
            _message = null;
            this.lstud = lstud;
            this.lstlive = lstlive;
            this.arg = arg;
        }

        public async Task<bool> Execute()
        {
            bool sub2nothing = true;
            string servers = "";
            if (!arg.Author.IsBot)
            {
                EmbedBuilder embedBuilder = new EmbedBuilder
                {
                    Title = "/abo",
                    Color = new Color(245, 107, 0)
                };
                embedBuilder.WithDescription("<@" + arg.Author.Id + ">");
                embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
                embedBuilder.WithAuthor("ListforgeNotify abo");
                embedBuilder.WithFooter("(✿◠‿◠) thanks for using me");
                embedBuilder.WithTimestamp(DateTime.Now);

                foreach (var ld in lstlive)
                {
                    foreach (var ud in lstud)
                    {
                        if (ud.Abo == true && ud.ServerId == ld.Id && Convert.ToUInt64(ud.AuthorId) == arg.Author.Id)
                        {
                            servers += "/" + ld.Name + "\n";
                            sub2nothing = false;
                        }
                    }
                }
                embedBuilder.AddField(servers, "These");
                if(sub2nothing)
                    embedBuilder.AddField("You are unsubscribed to everything", ":(");
                
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
