using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace BotDLL
{
    public class CSV_Connections
    {
        private static Uri _path = new Uri($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ListforgeBot");
        private static Uri _filepath = new Uri($"{_path}/Connections.csv");
        public static Connections ReadAll()
        {
            try
            {
                Connections cons = new Connections();
                StreamReader sr = new StreamReader(_filepath.LocalPath);
                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    string[] infos = row.Split(';');

                    switch (infos[0])
                    {
                        case "DiscordBotKey":
                            cons.DiscordBotKey = infos[1];
                            break;
                        case "DiscordBotKeyDebug":
                            cons.DiscordBotDebug = infos[1];
                            break;
                        case "TelegramBotKey":
                            cons.TelegramBotKey = infos[1];
                            break;
                        case "TelegramBotKeyDebug":
                            cons.TelegramBotKeyDebug = infos[1];
                            break;
                        case "MySqlConStr":
                            cons.MySqlConStr = infos[1].Replace(',', ';');
                            break;
                        case "MySqlConStrDebug":
                            cons.MySqlConStrDebug = infos[1].Replace(',', ';');
                            break;
                        case "QuickChartApi":
                            cons.QuickChartApi = infos[1];
                            break;
                        case "QuickChartApiDebug":
                            cons.QuickChartApiDebug = infos[1];
                            break;
                        default:
                            break;
                    }
                }
                sr.Close();
                return cons;
            }
            catch (Exception)
            {
                DirectoryInfo dir = new DirectoryInfo(_path.LocalPath);
                if (!dir.Exists)
                    dir.Create();

                StreamWriter streamWriter = new StreamWriter(_filepath.LocalPath);
                streamWriter.WriteLine( "DiscordBotKey;<API Key here>\n" +
                                        "DiscordBotKeyDebug;<API Key here>\n" +
                                        "TelegramBotKey;<API Key here>\n" +
                                        "MySqlConStr;<DBConnectionString here>\n" +
                                        "MySqlConStrDebug;<DBConnectionString here>\n" +
                                        "QuickChartApi;<API Key here>" +
                                        "QuickChartApiDebug;<API Key here>");

                streamWriter.Close();
                throw new Exception($"{_path.LocalPath}\n" +
                                    $"API Keys and Database string not configurated!");
            }
        }
    }
}
