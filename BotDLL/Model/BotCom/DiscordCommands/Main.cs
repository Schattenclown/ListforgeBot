﻿using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    /// <summary>
    /// The MAIN.
    /// </summary>
    internal class Main : BaseCommandModule 
    {
        /// <summary>
        /// prob. does nothing
        /// </summary>
        /// <param name="ctx">The ctx.</param>
        /// <returns>A Task.</returns>
        [Command("ping"), Description("Ping")]
        public async Task PingAsync(CommandContext ctx)
        {
            await ctx.RespondAsync($"{ctx.Client.Ping}ms");
        }
    }
}
