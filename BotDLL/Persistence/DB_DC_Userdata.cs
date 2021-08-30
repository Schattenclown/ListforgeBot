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
            String sql = "SELECT * FROM DC_Userdata";
            List<DC_Userdata> lst = new List<DC_Userdata>();
            MySqlConnection connection = DB_Connection.OpenDB();
            MySqlDataReader dataReader = DB_Connection.ExecuteReader(sql, connection);
            if (dataReader == null)
                return null;

            while (dataReader.Read())
            {
                DC_Userdata ud = new DC_Userdata
                {
                    AuthorId = dataReader.GetString("AuthorId"),
                    ChannelId = dataReader.GetString("ChannelId"),
                    Author = dataReader.GetString("Author"),
                    ServerId = dataReader.GetInt32("ServerId"),
                    Abo = dataReader.GetBoolean("Abo")
                };

                lst.Add(ud);
            }

            DB_Connection.CloseDB(connection);
            return lst;
        }
        public static void Add(DC_Userdata ud, bool notification)
        {
            String sql = $"INSERT INTO DC_Userdata (AuthorId, ChannelId, Author, ServerId, Abo)" +
                         $"VALUES ('{ud.AuthorId}', '{ud.ChannelId}', '{ud.Author}', {ud.ServerId}, {ud.Abo})";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void Change(DC_Userdata ud, bool notification)
        {
            String sql = $"UPDATE DC_Userdata SET Abo={ud.Abo}, Author='{ud.Author}' WHERE AuthorId={ud.AuthorId} AND ChannelId={ud.ChannelId} AND ServerId={ud.ServerId}";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void CreateTable_Userdata(bool notification)
        {
            string sql = "CREATE DATABASE IF NOT EXISTS `db_listforge`;" +
                            "USE `db_listforge`;" +
                            "CREATE TABLE IF NOT EXISTS `DC_Userdata` (" +
                            "`AuthorId` varchar(50) NOT NULL," +
                            "`ChannelId` varchar(50) DEFAULT NULL," +
                            "`Author` varchar(50) DEFAULT NULL," +
                            "`ServerId` int(11) DEFAULT NULL," +
                            "`Abo` int(11) DEFAULT NULL" +
                            ") ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;";

            DB_Connection.ExecuteNonQuery(sql, notification);
        }
    }
}
