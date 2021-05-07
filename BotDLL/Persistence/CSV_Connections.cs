﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class CSV_Connections
    {
        private static Uri Path = new Uri($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ListforgeBot/Connections.csv");
        public static Connections ReadAll()
        {
            Connections cons = new Connections();
            StreamReader sr = new StreamReader(Path.LocalPath);
            
            while (!sr.EndOfStream)
            {
                string row = sr.ReadLine();
                string[] infos = row.Split(';');
    
                switch (infos[0])
                {
                    case "DiscordBotKey":
                            cons.DiscordBotKey = infos[1];
                            break;
                    case "TelegramBotKey":
                            cons.TelegramBotKey = infos[1];
                        break;
                    case "DBConnectionString":
                            cons.MySqlConStr = infos[1].Replace(',', ';');
                        break;
                    case "QuickChartApi":
                        cons.QuickChartApi = infos[1];
                        break;
                    default:
                        break;
                }
            }
            sr.Close();
            return cons;
        }
    }
}