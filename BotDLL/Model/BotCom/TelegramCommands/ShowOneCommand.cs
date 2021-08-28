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
    public class ShowOneCommand : ITelegramCommandAsync
    {
        private readonly ITelegramBotClient botClient;
        private readonly ChatId chatId;
        private readonly List<LF_ServerInfo> lstlive;
        private readonly string message;

        public ShowOneCommand(ITelegramBotClient botClient, ChatId chatId, List<LF_ServerInfo> lstlive, string message)
        {
            this.botClient = botClient;
            this.chatId = chatId;
            this.lstlive = lstlive;
            this.message = message;
        }

        public async Task<bool> Execute()
        {
            string rightinfo = "";

            foreach (var item in lstlive)
            {
                if (item.Name == message)
                {
                    String onoff = "Offline";
                    if (item.Is_online == true)
                        onoff = "Online";
                    try
                    {
                        if (item.Name != "TheForest")
                            rightinfo = $"[Header]({item.LF_HeaderImgURi})\n" +
                                        $"Name: {item.Name}\n" +
                                        $"IP: {item.Address}:{item.Port}\n" +
                                        $"Status: {onoff}\n" +
                                        $"Players: {item.Players}/{item.Maxplayers}\n" +
                                        $"Version {item.Version}\n" +
                                        $"Uptime: {item.Uptime}%\n" +
                                        $"[ListForge]({item.LF_Uri})\n" +
                                        $"Last check: {item.Last_check}\n" +
                                        $"Last online: {item.Last_online}";
                        else
                            rightinfo = $"[Header]({item.LF_HeaderImgURi})\n" +
                                        $"Name: {item.Name}\n" +
                                        $"IP: {item.Address}:{item.Port}\n" +
                                        $"Status: {onoff}\n" +
                                        $"Players: {item.Players}/{item.Maxplayers}\n" +
                                        $"Version {item.Version.Replace("theforestDS ", "")}\n" +
                                        $"Uptime: {item.Uptime}%\n" +
                                        $"Last check: {item.Last_check}\n" +
                                        $"Last online: {item.Last_online}";
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            await botClient.SendTextMessageAsync(chatId: chatId, text: rightinfo, Telegram.Bot.Types.Enums.ParseMode.Markdown);

            return true;
        }

        public Task<string> GetMessage()
        {
            return Task.FromResult(string.Empty);
        }
    }
}
