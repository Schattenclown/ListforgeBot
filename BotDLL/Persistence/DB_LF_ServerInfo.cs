using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class DB_LF_ServerInfo
    {
        public static bool InserInto(string db, string Id, string Name, string Address, string Port, string Hostname, string Map, string Is_online, string Players, string Maxplayers, string Versions, string Uptime, DateTime Last_check, DateTime Last_online, string Key, string LF_Uri, string LF_StatUri, string QC_StatUri, Uri LF_HeaderImgUri, bool notification)
        {
            if (Id == null)
                return false;

            bool isthere = IsThere(db, Convert.ToInt32(Id));

            try
            {
                if (isthere == false)
                {
                    string sql = $"INSERT INTO {db} (Id, Name, Address, Port, Hostname, Map, Is_online, Players, Maxplayers, Version, Uptime, Last_check, Last_online, LF_Uri)" +
                                $"VALUES ({Id}, '{Name}', '{Address}', {Port}, '{Hostname}', '{Map}', {Is_online}, {Players}, {Maxplayers}, '{Versions.Replace("theforestDS ", "")}', {Uptime}, '{Last_check:yyyyy-MM-dd HH-mm}', '{Last_online:yyyyy-MM-dd HH-mm}', '{LF_Uri}')";
                    DB_Connection.ExecuteNonQuery(sql, notification);
                    UpdateSmoll(db, Id, LF_StatUri, QC_StatUri, LF_HeaderImgUri, notification);
                }
                else
                    Update(db, Id, Name, Address, Port, Hostname, Map, Is_online, Players, Maxplayers, Versions, Uptime, Last_check, Last_online, Key, LF_Uri, LF_StatUri, QC_StatUri, LF_HeaderImgUri, notification);
            }
            catch (Exception ex)
            {
                DebugLog.Log("ERROR: DB_LF_ServerInfo.InserInto:" + ex.Message);
                return false;
            }
            return true;
        }
        public static void Update(string swhatdb, string Id, string Name, string Address, string Port, string Hostname, string Map, string Is_online, string Players, string Maxplayers, string Versions, string Uptime, DateTime Last_check, DateTime Last_online, string LF_Uri, string Key, string LF_StatUri, string QC_StatUri, Uri LF_HeaderImgUri, bool notification)
        {
            string sql1 =   $"UPDATE {swhatdb} SET Id={Id}, Name='{Name}', Address='{Address}', Port={Port}, Hostname='{Hostname}', Map='{Map}', Is_online={Is_online}, Players={Players}, Maxplayers={Maxplayers}, Version='{Versions.Replace("theforestDS ", "")}', " +
                            $"Uptime={Uptime}, Last_check='{Last_check:yyyyy-MM-dd HH-mm}', Last_online='{Last_online:yyyyy-MM-dd HH-mm}', LF_Uri='{LF_Uri}', `key`='{Key}' WHERE ID={Id}";
            DB_Connection.ExecuteNonQuery(sql1, notification);
            
            string sql2 =   $"UPDATE {swhatdb} SET LF_StatUri='{LF_StatUri}', QC_StatUri='{QC_StatUri}', LFHeaderImgUri='{LF_HeaderImgUri}' WHERE ID={Id}";
            DB_Connection.ExecuteNonQuery(sql2, notification);
        }
        public static void UpdateSmoll(string swhatdb, string Id, string LF_StatUri, string QC_StatUri, Uri LF_HeaderImgUri, bool notification)
        {
            string sql = $"UPDATE {swhatdb} SET LF_StatUri='{LF_StatUri}', QC_StatUri='{QC_StatUri}', LFHeaderImgUri='{LF_HeaderImgUri}' WHERE ID={Id}";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static bool IsThere(string swhatdb, int id)
        {
            bool isthere = false;
            string sql = $"SELECT * FROM {swhatdb}";
            MySqlConnection connection = DB_Connection.OpenDB();
            MySqlDataReader dataReader = DB_Connection.ExecuteReader(sql, connection);
            if (dataReader == null)
                return true;

            while (dataReader.Read())
            {
                if (id == dataReader.GetInt32("Id"))
                    isthere = true;
            }
            DB_Connection.CloseDB(connection);
            return isthere;
        }
        public static List<LF_ServerInfo> ReadAll(string db)
        {
            List<LF_ServerInfo> lst = new List<LF_ServerInfo>();

            String sql = $"SELECT * FROM {db}";
            MySqlConnection connection = DB_Connection.OpenDB();
            MySqlDataReader rdr = DB_Connection.ExecuteReader(sql, connection);

            while (rdr.Read())
            {
                LF_ServerInfo obj = new LF_ServerInfo
                {
                    Id = rdr.GetInt32("ID"),
                    Name = rdr.GetString("Name"),
                    Address = rdr.GetString("Address"),
                    Port = rdr.GetInt32("Port"),
                    Hostname = rdr.GetString("Hostname"),
                    Map = rdr.GetString("Map"),
                    Is_online = rdr.GetBoolean("Is_online"),
                    Players = rdr.GetInt32("Players"),
                    Maxplayers = rdr.GetInt32("Maxplayers"),
                    Version = rdr.GetString("Version"),
                    Uptime = rdr.GetInt32("Uptime"),
                    Last_check = rdr.GetDateTime("Last_check"),
                    Last_online = rdr.GetDateTime("Last_online"),
                    Key = rdr.GetString("key"),
                    LF_Uri = new Uri(rdr.GetString("LF_Uri")),
                    LF_StatUri = new Uri(rdr.GetString("LF_StatUri")),
                    QC_StatUri = new Uri(rdr.GetString("QC_StatUri")),
                    LF_HeaderImgURi = new Uri(rdr.GetString("LFHeaderImgUri"))
                };
                lst.Add(obj);
            }
            DB_Connection.CloseDB(connection);

            return lst;
        }
        public static void Read(LF_ServerInfo liveInfo, string Key)
        {
            MySqlConnection con = DB_Connection.OpenDB();
            MySqlDataReader rdr = DB_Connection.ExecuteReader($"SELECT * FROM LF_ServerInfoLive WHERE `Key` = '{Key}'", con);
            if (rdr.Read())
            {
                GetDataFromReader(liveInfo, rdr);
            }
            rdr.Close();
            DB_Connection.CloseDB(con);
        }
        private static void GetDataFromReader(LF_ServerInfo liveInfo, MySqlDataReader rdr)
        {
            liveInfo.Id = rdr.GetInt32("ID");
            liveInfo.Name = rdr.GetString("Name");
            liveInfo.Address = rdr.GetString("Address");
            liveInfo.Port = rdr.GetInt32("Port");
            liveInfo.Hostname = rdr.GetString("Hostname");
            liveInfo.Map = rdr.GetString("Map");
            liveInfo.Is_online = rdr.GetBoolean("Is_online");
            liveInfo.Players = rdr.GetInt32("Players");
            liveInfo.Maxplayers = rdr.GetInt32("Maxplayers");
            liveInfo.Version = rdr.GetString("Version");
            liveInfo.Uptime = rdr.GetInt32("Uptime");
            liveInfo.Last_check = rdr.GetDateTime("Last_check");
            liveInfo.Last_online = rdr.GetDateTime("Last_online");
            liveInfo.Key = rdr.GetString("key");
            liveInfo.LF_Uri = new Uri(rdr.GetString("LF_Uri"));
            liveInfo.LF_StatUri = new Uri(rdr.GetString("LF_StatUri"));
            liveInfo.QC_StatUri = new Uri(rdr.GetString("QC_StatUri"));
            liveInfo.LF_HeaderImgURi = new Uri(rdr.GetString("LFHeaderImgUri"));
        }
        public static void DeleteAll(bool notification)
        {
            string sql = "DELETE FROM LF_ServerInfoLive";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void Delete(LF_API_Uri aPI_URL, bool notification)
        {
            string sql = $"DELETE FROM LF_ServerInfoLive WHERE `Key` = '{aPI_URL.Key}'";
            DB_Connection.ExecuteNonQuery(sql, notification);
        }
        public static void CreateTable_LF_ServerInfoLive(bool notification)
        {
            string sql = "CREATE DATABASE IF NOT EXISTS `db_listforge`;" +
                            "USE `db_listforge`;" +
                            "" +
                            "CREATE TABLE IF NOT EXISTS `LF_ServerInfoLive` (" +
                            "`Id` int(11) NOT NULL," +
                            "`Name` varchar(50) DEFAULT NULL," +
                            "`Address` varchar(50) DEFAULT NULL," +
                            "`Port` int(11) DEFAULT NULL," +
                            "`Hostname` varchar(50) DEFAULT NULL," +
                            "`Map` varchar(50) DEFAULT NULL," +
                            "`Is_online` int(11) DEFAULT NULL," +
                            "`Players` int(11) DEFAULT NULL," +
                            "`Maxplayers` int(11) DEFAULT NULL," +
                            "`Version` varchar(50) DEFAULT NULL," +
                            "`Uptime` int(11) DEFAULT NULL," +
                            "`Last_check` datetime DEFAULT NULL," +
                            "`Last_online` datetime DEFAULT NULL," +
                            "`Key` varchar(50) DEFAULT NULL," +
                            "`LF_Uri` varchar(50) DEFAULT NULL," +
                            "`LF_StatUri` varchar(150) DEFAULT NULL," +
                            "`QC_StatUri` varchar(6666) DEFAULT NULL," +
                            "`LFHeaderImgUri`varchar(6666) Default Null," +
                            "PRIMARY KEY (`Id`)," +
                            "KEY `Key` (`Key`)" +
                            ") ENGINE=InnoDB DEFAULT CHARSET=latin1;";

            DB_Connection.ExecuteNonQuery(sql, notification);
        }
    }
}
