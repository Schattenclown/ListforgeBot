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
            String sql = "SELECT * FROM Userdata";
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
        public static void Add(TLG_Userdata ud)
        {
            String sql = $"INSERT INTO Userdata (UserNr, ChatId, Username, ServerId, Abo)" +
                         $"VALUES ({ud.UserNr}, {ud.ChatId}, '{ud.Username}', {ud.ServerId}, {ud.Abo})";
            DB_Connection.ExecuteNonQuery(sql);
        }
        public static void Change(TLG_Userdata ud)
        {
            String sql = $"UPDATE Userdata SET Abo={ud.Abo} WHERE ChatId={ud.ChatId} AND ServerId={ud.ServerId}";
            DB_Connection.ExecuteNonQuery(sql);
        }
    }
}
