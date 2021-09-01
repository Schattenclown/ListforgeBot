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
            List<UInt64> differentchannel = new List<ulong>();
            string servers = "";
            List<DC_Abo> lstDC_Abo = new List<DC_Abo>();
            List<DC_Abo> lstDC_Abo_sort = new List<DC_Abo>();

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
                        if (ud.Abo && ud.ServerId == ld.Id && Convert.ToUInt64(ud.AuthorId) == arg.Author.Id)
                        {
                            DC_Abo dcabo = new DC_Abo(Convert.ToUInt64(ud.ChannelId), Convert.ToUInt64(ud.AuthorId), ld.Name);

                            if (!differentchannel.Contains(Convert.ToUInt64(ud.ChannelId)) && Convert.ToUInt64(ud.AuthorId) == arg.Author.Id)
                                differentchannel.Add(Convert.ToUInt64(ud.ChannelId));

                            lstDC_Abo.Add(dcabo);

                            sub2nothing = false;
                        }
                    }
                }
                
                if(sub2nothing)
                    embedBuilder.AddField("You are unsubscribed to everything", ":(");
                else
                {
                    foreach (var diffchn in differentchannel)
                    {
                        foreach (var dC_Abo in lstDC_Abo)
                        {

                            if (!lstDC_Abo_sort.Contains(dC_Abo) && dC_Abo.ChannelId == diffchn)
                            {
                                lstDC_Abo_sort.Add(dC_Abo);
                            }
                        }
                    }
                }

                UInt64 lastchn = 0;

                foreach (var item in lstDC_Abo_sort)
                {
                    if (lastchn == 0)
                        lastchn = item.ChannelId;

                    if (!servers.Contains(item.Server))
                        servers += "/" + item.Server + "\n";

                    if (lastchn != item.ChannelId)
                    {
                        embedBuilder.AddField(servers, "<#" + lastchn.ToString() + ">");
                        servers = "";
                        servers += "/" + item.Server + "\n";
                        lastchn = item.ChannelId;
                    }

                    if (lstDC_Abo_sort.Last() == item)
                    {
                        embedBuilder.AddField(servers, "<#" + lastchn.ToString() + ">");
                    }
                }

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
