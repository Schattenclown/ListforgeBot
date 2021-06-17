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
    public class ShowAllCommand : ITelegramCommandAsync
    {
        private readonly ITelegramBotClient botClient;
        private readonly ChatId chatId;
        private readonly List<LF_ServerInfo> lstlive;

        public ShowAllCommand(ITelegramBotClient botClient, ChatId chatId, List<LF_ServerInfo> lstlive)
        {
            this.botClient = botClient;
            this.chatId = chatId;
            this.lstlive = lstlive;
        }

        public async Task<bool> Execute()
        {
            foreach (var item in lstlive)
            {
                String onoff = "Offline"; 
                if (item.Is_online == true) 
                    onoff = "Online";

                if (item.Name != "TheForest")
                    await botClient.SendTextMessageAsync(chatId: chatId, text: $"Name: {item.Name}\n" +
                                                                                $"IP: {item.Address}:{item.Port}\n" +
                                                                                $"Status: {onoff}\n" +
                                                                                $"Players: {item.Players}/{item.Maxplayers}\n" +
                                                                                $"Version {item.Version}\n" +
                                                                                $"Uptime: {item.Uptime}%\n" +
                                                                                $"{item.LF_Uri}\n" +
                                                                                $"Last check: {item.Last_check}\n" +
                                                                                $"Last online: {item.Last_online}\n" +
                                                                                $"{item.LF_HeaderImgURi}");
                else
                    await botClient.SendTextMessageAsync(chatId: chatId, text: $"Name: {item.Name}\n" +
                                                                                $"IP: {item.Address}:{item.Port}\n" +
                                                                                $"Status: {onoff}\n" +
                                                                                $"Players: {item.Players}/{item.Maxplayers}\n" +
                                                                                $"Version {item.Version.Replace("theforestDS ", "")}\n" +
                                                                                $"Uptime: {item.Uptime}%\n" +
                                                                                $"Last check: {item.Last_check}\n" +
                                                                                $"Last online: {item.Last_online}\n" +
                                                                                $"{item.LF_HeaderImgURi}");
            }

            return true;
        }

        public Task<string> GetMessage()
        {
            return Task.FromResult(string.Empty);
        }
    }
}
