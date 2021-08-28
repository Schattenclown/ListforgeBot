using BotDLL.Model.BotCom.TelegramCommands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.TelegramCommands
{
    public class AddCommand : ITelegramCommandAsync
    {
        private string _message;

        public AddCommand(string addme)
        {
            _message = GenerateMessage(addme);
        }

        private string GenerateMessage(string addme)
        {
            return $"About wich Servers do you want to get Informed?\n\n{addme}";
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
