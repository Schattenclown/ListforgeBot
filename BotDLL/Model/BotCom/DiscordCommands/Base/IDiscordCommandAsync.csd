using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands.Base
{
    public interface IDiscordCommandAsync
    {
        Task<Embed> GetMessage();
        Task<bool> Execute();
    }
}
