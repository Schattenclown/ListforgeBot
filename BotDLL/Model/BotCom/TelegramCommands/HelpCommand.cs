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

        public HelpCommand(string servers)
        {
            _message = GenerateMessage(servers);
        }

        private string GenerateMessage(string servers)
        {
            return $"Type /help for help obviously!\n" +
                $"\n" +
                $"/42 All ServerInfo.!\n" +
                $"/42lk All Serverinfo. but lowkey!\n" +
                $"\n" +
                $"{servers}\n" +
                $"/add Be informed if serverstats change!\n" +
                $"/del Unsubscribe from serverstats!\n" +
                $"/abo What am i subscribed to!\n" +
                $"\n" +
                $"(✿◠‿◠) thx for using me!";
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
