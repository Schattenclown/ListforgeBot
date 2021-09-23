using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    /// <summary>
    /// The Abotype choice provider.
    /// </summary>
    public class AboTypeChoiceProvider : IChoiceProvider
    {
        /// <summary>
        /// Providers the choices.
        /// </summary>
        /// <returns>choices</returns>
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[2];

            choices[0] = new DiscordApplicationCommandOptionChoice("FULL", "0");
            choices[1] = new DiscordApplicationCommandOptionChoice("MINIMAL", "1");

            return choices;
        }
    }
}
