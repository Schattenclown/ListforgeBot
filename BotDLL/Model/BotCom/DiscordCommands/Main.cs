using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    internal class Main : BaseCommandModule 
    {
        
        [Command("ping"), Description("Ping")]
        public async Task PingAsync(CommandContext ctx)
        {
            await ctx.RespondAsync($"{ctx.Client.Ping}ms");
        }
    }
}
