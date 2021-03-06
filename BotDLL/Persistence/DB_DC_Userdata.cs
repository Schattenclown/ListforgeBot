using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BotDLL
{
    public class DB_DC_Userdata
    {
        public static List<DC_Userdata> ReadAll()
        {
            string sql = "SELECT * FROM DC_Userdata";
            List<DC_Userdata> lst = new List<DC_Userdata>();
            MySqlConnection connection = DB_Connection.OpenDB();
            MySqlDataReader dataReader = DB_Connection.ExecuteReader(sql, connection);
            if (dataReader == null)
                return null;

            while (dataReader.Read())
            {
                DC_Userdata ud = new DC_Userdata
                {
                    AuthorId = dataReader.GetUInt64("AuthorId"),
                    ChannelId = dataReader.GetUInt64("ChannelId"),
                    Author = dataReader.GetString("Author"),
                    ServerId = dataReader.GetInt32("ServerId"),
                    Abo = dataReader.GetBoolean("Abo"),
                    MinimalAbo = dataReader.GetBoolean("MinimalAbo")
                };

                lst.Add(ud);
            }

            DB_Connection.CloseDB(connection);
            return lst;
        }
        public static void Add(DC_Userdata ud, bool showMessageBox)
        {
            String sql = $"INSERT INTO DC_Userdata (AuthorId, ChannelId, Author, ServerId, Abo, MinimalAbo)" +
                         $"VALUES ('{ud.AuthorId}', '{ud.ChannelId}', '{ud.Author}', {ud.ServerId}, {ud.Abo}, {ud.MinimalAbo})";
            DB_Connection.ExecuteNonQuery(sql, showMessageBox);
        }
        public static void Change(DC_Userdata ud, bool showMessageBox)
        {
            String sql = $"UPDATE DC_Userdata SET Abo={ud.Abo}, MinimalAbo={ud.MinimalAbo}, Author='{ud.Author}' WHERE AuthorId={ud.AuthorId} AND ChannelId={ud.ChannelId} AND ServerId={ud.ServerId}";
            DB_Connection.ExecuteNonQuery(sql, showMessageBox);
        }
        public static void CreateTable_Userdata(bool showMessageBox)
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
                            "CREATE TABLE IF NOT EXISTS `DC_Userdata` (" +
                            "`AuthorId` varchar(50) NOT NULL," +
                            "`ChannelId` varchar(50) DEFAULT NULL," +
                            "`Author` varchar(50) DEFAULT NULL," +
                            "`ServerId` int(11) DEFAULT NULL," +
                            "`Abo` int(11) DEFAULT NULL," +
                            "`MinimalAbo` int(11) DEFAULT NULL" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;";

            DB_Connection.ExecuteNonQuery(sql, showMessageBox);
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
