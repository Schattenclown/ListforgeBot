using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class Connections
    {
        public string DiscordBotKey { get; set; }
        public string TelegramBotKey { get; set; }
        public string MySqlConStr { get; set; }
        public string QuickChartApi { get; set; }

        public static Connections GetConnections()
        {
            return CSV_Connections.ReadAll();
        }
    }
}
