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
        public static List<TLG_Userdata> ReadAll()
        {
            String sql = "SELECT * FROM TL_USerdata";
            List<TLG_Userdata> lst = new List<TLG_Userdata>();
            MySqlConnection connection = DB_Connection.OpenDB();
            MySqlDataReader dataReader = DB_Connection.ExecuteReader(sql, connection);
            if (dataReader == null)
                return null;

            while (dataReader.Read())
            {
                TLG_Userdata ud = new TLG_Userdata
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
        public static void Add(TLG_Userdata ud, bool notification)
        {
            String sql = $"INSERT INTO TL_USerdata (UserNr, ChatId, Username, ServerId, Abo)" +
                         $"VALUES ({ud.UserNr}, {ud.ChatId}, '{ud.Username}', {ud.ServerId}, {ud.Abo})";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void Change(TLG_Userdata ud, bool notification)
        {
            String sql = $"UPDATE TL_USerdata SET Abo={ud.Abo} WHERE ChatId={ud.ChatId} AND ServerId={ud.ServerId}";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void CreateTable_Userdata(bool notification)
        {
            string sql = "CREATE DATABASE IF NOT EXISTS `db_listforge`;" +
                            "USE `db_listforge`;" +
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
    }
}
