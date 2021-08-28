using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.TelegramCommands.Base
{
    public interface ITelegramCommandAsync
    {
        Task<string> GetMessage();
        Task<bool> Execute();
    }
}
