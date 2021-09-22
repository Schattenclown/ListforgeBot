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
    public class AboStateChoiceProvider : IChoiceProvider
    {
        /// <summary>
        /// This is the Database Table name.
        /// </summary>
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[2];

            choices[0] = new DiscordApplicationCommandOptionChoice("FULL", "0");
            choices[1] = new DiscordApplicationCommandOptionChoice("LOWKEY", "1");

            return choices;
        }
    }
}
