using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BotDLL
{
    public class Connections
    {
        public string DiscordBotKey { get; set; }
        public string DiscordBotDebug { get; set; }
        public string TelegramBotKey { get; set; }
        public string TelegramBotKeyDebug { get; set; }
        public string MySqlConStr { get; set; }
        public string MySqlConStrDebug { get; set; }
        public string QuickChartApi { get; set; }
        public string QuickChartApiDebug { get; set; }

        public static Connections GetConnections()
        {
            try
            {
                return CSV_Connections.ReadAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message, "Error");
                throw ex;
            }
        }
    }
}
