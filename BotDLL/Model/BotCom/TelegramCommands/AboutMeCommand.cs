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
    public class AboutMeCommand : ITelegramCommandAsync
    {
        private readonly ITelegramBotClient botClient;
        private readonly List<TLG_Userdata> lstud;
        private readonly List<LF_ServerInfo> lstlive;
        private readonly ChatId chatId;

        public AboutMeCommand(ITelegramBotClient botClient, List<TLG_Userdata> lstud, List<LF_ServerInfo> lstlive, ChatId chatId)
        {
            this.botClient = botClient;
            this.lstud = lstud;
            this.lstlive = lstlive;
            this.chatId = chatId;
        }

        public async Task<bool> Execute()
        {
            foreach (var si in lstlive)
            {
                foreach (var ud in lstud)
                {
                    if ((ChatId)ud.ChatId == chatId && ud.Abo == true && ud.ServerId == si.Id)
                        await botClient.SendTextMessageAsync(chatId: chatId, text: $"{si.Name}");
                }
            }

            return true;
        }

        public Task<string> GetMessage()
        {
            return Task.FromResult(string.Empty);
        }
    }
}
