using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class DB_LF_API_Uri
    {
        public static List<LF_API_Uri> ReadAll()
        {
            List<LF_API_Uri> lst = new List<LF_API_Uri>();
            MySqlConnection con = DB_Connection.OpenDB();
            MySqlDataReader rdr = DB_Connection.ExecuteReader("SELECT * FROM LF_API_Uri", con);
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    LF_API_Uri aPI = new LF_API_Uri();
                    GetDataFromReader(aPI, rdr);
                    lst.Add(aPI);
                }
                rdr.Close();
                DB_Connection.CloseDB(con);
            }

            return lst;
        }
        private static void GetDataFromReader(LF_API_Uri aPI, MySqlDataReader rdr)
        {
            aPI.URLID = rdr.GetInt32("UriNr");
            aPI.URL = new Uri(rdr.GetString("Uri"));
            aPI.Key = rdr.GetString("Key");
            aPI.Combined = new Uri(rdr.GetString("Combined"));
            LF_ServerInfo liveInfo = new LF_ServerInfo(aPI.Key);
            aPI.Info = liveInfo;
        }
        public static void Write(LF_API_Uri aPI_URL, bool notification)
        {

            switch (aPI_URL.Game)
            {
                case Game.ARK:
                    aPI_URL.URL = new Uri("https://ark-servers.net/api/?object=servers&element=detail&key=");
                    break;
                case Game.Conan:
                    aPI_URL.URL = new Uri("https://conan-exiles.com/api/?object=servers&element=detail&key=");
                    break;
                case Game.CSGO:
                    aPI_URL.URL = new Uri("https://counter-strike-servers.net/api/?object=servers&element=detail&key=");
                    break;
                case Game.Minecraft:
                    aPI_URL.URL = new Uri("https://minecraft-mp.com/api/?object=servers&element=detail&key=");
                    break;
                case Game.TeamSpeak:
                    aPI_URL.URL = new Uri("https://teamspeak-servers.org/api/?object=servers&element=detail&key=");
                    break;
                case Game.Valheim:
                    aPI_URL.URL = new Uri("https://valheim-servers.io/api/?object=servers&element=detail&key=");
                    break;
                default:
                    break;
            }
            aPI_URL.Combined = new Uri($"{aPI_URL.URL}{aPI_URL.Key}");

            string sql = $"INSERT INTO LF_API_Uri(Uri, `Key`, Combined) VALUES('{aPI_URL.URL}', '{aPI_URL.Key}', '{aPI_URL.Combined}')";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void Delete(LF_API_Uri aPI_URL, bool notification)
        {
            string sql = $"DELETE FROM LF_API_Uri WHERE `Key` = '{aPI_URL.Key}'";

            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void CreateTable_LF_API_Uri(bool notification)
        {
            CSV_Connections cSV_Connections = new CSV_Connections();
            Connections cons = new Connections();
            cons = CSV_Connections.ReadAll();

            string database = RemoveTillWord(cons.MySqlConStr, "Database=", 9);
#if DEBUG
            database = RemoveTillWord(cons.MySqlConStrDebug, "Database=", 9);
#endif
            database = RemoveAfterWord(database, "; Uid", 0);

            string sql = $"CREATE DATABASE IF NOT EXISTS `{database}`;" +
                            $"USE `{database}`;" +
                            "CREATE TABLE IF NOT EXISTS `LF_API_Uri` (" +
                            "`UriNr` int(11) NOT NULL AUTO_INCREMENT," +
                            "`Uri` varchar(200) DEFAULT NULL," +
                            "`Key` varchar(200) DEFAULT NULL," +
                            "`Combined` varchar(200) DEFAULT NULL," +
                            "PRIMARY KEY (`UriNr`) USING BTREE" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=latin1;";

            DB_Connection.ExecuteNonQuery(sql, notification);
        }

        public static string RemoveTillWord(string input, string word, int removewordint)
        {
            return input.Substring(input.IndexOf(word) + removewordint);
        }
        public static string RemoveAfterWord(string input, string word, int keepwordint)
        {
            int index = input.LastIndexOf(word);
            if (index > 0)
                input = input.Substring(0, index + keepwordint);

            return input;
        }
    }
}
