using DisCatSharp.ApplicationCommands;
using DisCatSharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotDLL.Model.BotCom.DiscordCommands
{
    /// <summary>
    /// The Servername choice provider.
    /// </summary>
    public class ServerNameChoiceProvider : IChoiceProvider
    {
        /// <summary>
        /// This is the Database Table name.
        /// </summary>
        private const string db = "LF_ServerInfoLive";
        /// <summary>
        /// Providers the choices.
        /// </summary>
        /// <returns>choices</returns>
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            List<LF_ServerInfo> lstlive = LF_ServerInfo.ReadAll(db);
            string servers = "";

            foreach (var item in lstlive)
            {
                servers += item.Name.ToLower() + ";";
            }
            string[] arrServers = servers.Trim(';').Split(';');

            DiscordApplicationCommandOptionChoice[] choices = new DiscordApplicationCommandOptionChoice[arrServers.Length];

            int i = 0;

            foreach (var item in arrServers)
            {
                choices[i] = new DiscordApplicationCommandOptionChoice(item.ToUpper(), i.ToString());
                i++;
            }

            return choices;
        }
    }
}
