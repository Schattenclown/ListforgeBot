using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BotDLL
{
    public class DB_TL_Userdata
    {
        public static List<TL_Userdata> ReadAll()
        {
            String sql = "SELECT * FROM TL_USerdata";
            List<TL_Userdata> lst = new List<TL_Userdata>();
            MySqlConnection connection = DB_Connection.OpenDB();
            MySqlDataReader dataReader = DB_Connection.ExecuteReader(sql, connection);
            if (dataReader == null)
                return null;

            while (dataReader.Read())
            {
                TL_Userdata ud = new TL_Userdata
                {
                    ChatId = dataReader.GetInt32("ChatId"),
                    Username = dataReader.GetString("Username"),
                    ServerId = dataReader.GetInt32("ServerId"),
                    Abo = dataReader.GetBoolean("Abo")
                };

                lst.Add(ud);
            }

            DB_Connection.CloseDB(connection);
            return lst;
        }
        public static void Add(TL_Userdata ud, bool notification)
        {
            String sql = $"INSERT INTO TL_USerdata (UserNr, ChatId, Username, ServerId, Abo)" +
                         $"VALUES ({ud.UserNr}, {ud.ChatId}, '{ud.Username}', {ud.ServerId}, {ud.Abo})";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void Change(TL_Userdata ud, bool notification)
        {
            String sql = $"UPDATE TL_USerdata SET Abo={ud.Abo} WHERE ChatId={ud.ChatId} AND ServerId={ud.ServerId}";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void CreateTable_Userdata(bool notification)
        {
            CSV_Connections cSV_Connections = new CSV_Connections();
            Connections cons = new Connections();
            cons = CSV_Connections.ReadAll();

            string database = RemoveTillWord(cons.MySqlConStr, "Database=", 9);
            database = RemoveAfterWord(database, "; Uid", 0);

            string sql = $"CREATE DATABASE IF NOT EXISTS `{database}`;" +
                            $"USE `{database}`;" +
                            "CREATE TABLE IF NOT EXISTS `TL_USerdata` (" +
                            "`UserNr` int(11) NOT NULL AUTO_INCREMENT," +
                            "`ChatId` int(11) DEFAULT NULL," +
                            "`Username` varchar(50) DEFAULT NULL," +
                            "`ServerId` int(11) DEFAULT NULL," +
                            "`Abo` int(11) DEFAULT NULL," +
                            "PRIMARY KEY (`UserNr`)" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;";

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
