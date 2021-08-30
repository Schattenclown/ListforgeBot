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
    public class ChangeSubscriptionCommand : IDiscordCommandAsync
    {
        private readonly SocketMessage arg;
        private readonly List<DC_Userdata> lstud;
        private readonly List<LF_ServerInfo> lstlive;
        private readonly string servername;
        private readonly string author;
        private readonly string authorId;
        private readonly string channelId;
        private readonly bool abo;

        private Embed _message;

        public ChangeSubscriptionCommand(List<DC_Userdata> lstud, List<LF_ServerInfo> lstlive, SocketMessage arg, string servername, bool abo)
        {
            _message = null;

            this.arg = arg;
            this.lstud = lstud;
            this.lstlive = lstlive;
            this.servername = servername;
            this.author = arg.Author.ToString();
            authorId = arg.Author.Id.ToString();
            channelId = arg.Channel.Id.ToString();
            this.abo = abo;
        }

        public async Task<bool> Execute()
        {

            int serverid = 0;
            string sabo;
            if (abo == true)
                sabo = $"You will get notifications for {servername}!";
            else
                sabo = $"You will no longer get notifications for {servername}!";

            foreach (var item in lstlive)
            {
                if (item.Name == servername.Replace("/add", "").Replace("/del", ""))
                {
                    serverid = item.Id;
                }
            }

            bool found = false;
            foreach (var item in lstud)
            {
                if (item.ChannelId == channelId && item.ServerId == serverid && item.AuthorId == authorId)
                    found = true;
            }

            if (!found)
            {
                DC_Userdata ud = new DC_Userdata
                {
                    AuthorId = authorId,
                    ChannelId = channelId,
                    Author = author,
                    ServerId = serverid,
                    Abo = abo,
                };
                DC_Userdata.Add(ud, false);
            }
            else if (found)
            {
                DC_Userdata ud = new DC_Userdata
                {
                    AuthorId = authorId,
                    ChannelId = channelId,
                    Author = author,
                    ServerId = serverid,
                    Abo = abo,
                };
                DC_Userdata.Change(ud, false);
            }

            if (!arg.Author.IsBot)
            {
                EmbedBuilder embedBuilder = new EmbedBuilder
                {
                    Title = $"{sabo}",
                    Color = new Color(245, 107, 0)
                };
                embedBuilder.AddField($"{arg.Author}", "You");
                embedBuilder.AddField($"{arg.Channel}", "In channel");
                embedBuilder.ThumbnailUrl = "https://i.imgur.com/eb0RgjI.png";
                embedBuilder.WithAuthor("ListforgeNotify abo");
                embedBuilder.WithFooter("(✿◠‿◠) thanks for using me");
                embedBuilder.WithTimestamp(DateTime.Now);

                await arg.Channel.SendMessageAsync(null, false, embedBuilder.Build());
            }

            return true;
        }

        public Task<Embed> GetMessage()
        {
            return Task.FromResult(_message);
        }
    }
}
