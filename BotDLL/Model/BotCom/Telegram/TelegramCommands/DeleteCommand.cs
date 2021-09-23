using BotDLL.Model.BotCom.TelegramCommands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.TelegramCommands
{
    public class DeleteCommand : ITelegramCommandAsync
    {
        private string _message;

        public DeleteCommand(string delme)
        {
            _message = GenerateMessage(delme);
        }

        private string GenerateMessage(string delme)
        {
            return $"What server do you wish to Silence?\n\n{delme}";
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
