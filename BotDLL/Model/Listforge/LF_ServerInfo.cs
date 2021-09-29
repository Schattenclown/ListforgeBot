using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotDLL
{
    public class LF_ServerInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Hostname { get; set; }
        public string Map { get; set; }
        public bool Is_online { get; set; }
        public int Players { get; set; }
        public int Maxplayers { get; set; }
        public string Version { get; set; }
        public int Uptime { get; set; }
        public DateTime Last_check { get; set; }
        public DateTime Last_online { get; set; }
        public string Key { get; set; }
        public Uri LF_Uri { get; set; }
        public Uri LF_StatUri { get; set; }
        public Uri QC_StatUri { get; set; }
        public Uri LF_HeaderImgURi { get; set; }

        public static List<LF_ServerInfo> ReadAll(string db)
        {
            return DB_LF_ServerInfo.ReadAll(db);
        }
        public LF_ServerInfo()
        {
            
        }
        public LF_ServerInfo(string Key)
        {
             DB_LF_ServerInfo.Read(this, Key);
        }
        public override string ToString()
        {
            return $"{Id,8} ██ {Name,-15} ██ {Address,20}:{Port,-5} ██ {Hostname,-30} ██ {Map,-20} ██ {Is_online,-6} ██ {Players,3}/{Maxplayers,-5} ██ {Version,10} ██ {Uptime,3}% ██  {Last_check,-19}  ██  {Last_online,-18}";
        }
        public static void DeleteAll(bool notification)
        {
            DB_LF_ServerInfo.DeleteAll(notification);
        }
        public static void CreateTable_LF_ServerInfoLive(bool notification)
        {
            DB_LF_ServerInfo.CreateTable_LF_ServerInfoLive(notification);
        }
    }
}
