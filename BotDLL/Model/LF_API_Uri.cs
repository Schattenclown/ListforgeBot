using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL

{
    public enum Game
    {
        ARK,
        Conan,
        CSGO,
        Minecraft,
        TeamSpeak,
        Valheim
    }
    public class LF_API_Uri
    {
        public Game Game { get; set; }
        public int URLID { get; set; }
        public Uri URL { get; set; }
        public string Key { get; set; }
        public Uri Combined { get; set; }
        public LF_ServerInfo Info { get; set; }
        public static List<LF_API_Uri> ReadAll()
        {
            return DB_LF_API_Uri.ReadAll();
        }
        public void Write(bool notification)
        {
            DB_LF_API_Uri.Write(this, notification);
            LF_Fetcher.Fetch("LF_ServerInfoLive");
        }
        public static void Delete(LF_API_Uri aPI_URL, bool notification)
        {
            DB_LF_API_Uri.Delete(aPI_URL, notification);
        }
        public static void CreateTable_LF_API_Uri(bool notification)
        {
             DB_LF_API_Uri.CreateTable_LF_API_Uri(notification);
        }
    }
}
