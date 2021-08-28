using BotDLL.Model.BotCom.TelegramCommands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotDLL.Model.BotCom.TelegramCommands
{
    public class ChangeSubscriptionCommand : ITelegramCommandAsync
    {
        private readonly ITelegramBotClient botClient;
        private readonly List<TLG_Userdata> lstud;
        private readonly List<LF_ServerInfo> lstlive;
        private readonly string servername;
        private readonly ChatId chatId;
        private readonly string username;
        private readonly bool abo;

        public ChangeSubscriptionCommand(ITelegramBotClient botClient, List<TLG_Userdata> lstud, List<LF_ServerInfo> lstlive, string servername, ChatId chatId, string username, bool abo)
        {
            this.botClient = botClient;
            this.lstud = lstud;
            this.lstlive = lstlive;
            this.servername = servername;
            this.chatId = chatId;
            this.username = username;
            this.abo = abo;
        }

        public async Task<bool> Execute()
        {
            int serverid = 0;
            string sabo = $"You will get notifications for {servername}!";

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
                if ((ChatId)item.ChatId == chatId && item.ServerId == serverid)
                    found = true;
            }

            if (found == false)
            {
                TLG_Userdata ud = new TLG_Userdata
                {
                    ChatId = Convert.ToInt32(chatId),
                    ServerId = serverid,
                    Abo = abo,
                };
                if (username != null)
                    ud.Username = username;
                else
                    ud.Username = "Noname";
                TLG_Userdata.Add(ud, false);
                await botClient.SendTextMessageAsync(chatId: chatId, text: sabo);
            }
            else if (found == true)
            {
                    TLG_Userdata ud = new TLG_Userdata
                    {
                        ChatId = Convert.ToInt32(chatId),
                        ServerId = serverid,
                        Abo = abo
                    };
                    if (username != null)
                        ud.Username = username;
                    else
                        ud.Username = "Noname";
                    TLG_Userdata.Change(ud, false);
                    await botClient.SendTextMessageAsync(chatId: chatId, text: sabo);
            }

            return true;
        }

        public Task<string> GetMessage()
        {
            return Task.FromResult(string.Empty);
        }
    }
}
