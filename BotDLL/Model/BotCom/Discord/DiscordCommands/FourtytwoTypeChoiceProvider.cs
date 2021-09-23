using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    /// <summary>
    /// The fourtytwo type choice provider.
    /// </summary>
    public class FourtytwoTypeChoiceProvider : IChoiceProvider
    {
        /// <summary>
        /// Providers the choices.
        /// </summary>
        /// <returns>choices</returns>
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[3];

            choices[0] = new DiscordApplicationCommandOptionChoice("FULL", "0");
            choices[1] = new DiscordApplicationCommandOptionChoice("MINIMAL", "1");
            choices[2] = new DiscordApplicationCommandOptionChoice("STATISTICS", "2");

            return choices;
        }
    }
}
