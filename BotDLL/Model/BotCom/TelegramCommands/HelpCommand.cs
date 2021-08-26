using BotDLL.Model.BotCom.TelegramCommands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.TelegramCommands
{
    public class HelpCommand : ITelegramCommandAsync
    {
        private string _message;

        public HelpCommand(string servers, string statisticsservers)
        {
            _message = GenerateMessage(servers, statisticsservers);
        }

        private string GenerateMessage(string servers, string statisticsservers)
        {
            return  $"/help shows help\n" +
                    $"\n" +
                    $"/42big show every server\n" +
                    $"/42 show every server with less info\n" +
                    $"/42s show playerstatistics for every server\n" +
                    $"\n" +
                    $"/list show serverlist\n" +
                    $"\n" +
                    $"Serverinfo from single server\n" +
                    $"{servers.ToLower()}" +
                    $"\n" +
                    $"Playerstatistics from single server\n" +
                    $"{statisticsservers.ToLower()}" +
                    $"\n" +
                    $"(✿◠‿◠) ty for using me";
        }

        public Task<bool> Execute()
        {
            return Task.FromResult(true);
        }

        public Task<string> GetMessage()
        {
            return Task.FromResult(_message);
        }
    }
}
