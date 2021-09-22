using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    /// <summary>
    /// The experiment choice provider.
    /// </summary>
    public class TestFunctionChoiceProvider : IChoiceProvider
    {
        /// <summary>
        /// This is the Database Table name.
        /// </summary>
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[3];

            choices[0] = new DiscordApplicationCommandOptionChoice("Player count changed", "0");
            choices[1] = new DiscordApplicationCommandOptionChoice("Status changed", "1");
            choices[2] = new DiscordApplicationCommandOptionChoice("Version changed", "2");

            return choices;
        }
    }
}
