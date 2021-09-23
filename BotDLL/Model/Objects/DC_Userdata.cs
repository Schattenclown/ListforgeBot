using System.Collections.Generic;

namespace BotDLL
{
    public class DC_Userdata
    {
        public ulong AuthorId { get; set; }
        public ulong ChannelId { get; set; }
        public string Author { get; set; }
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public bool Abo { get; set; }
        public bool MinimalAbo { get; set; }
        
        public static List<DC_Userdata> ReadAll()
        {
            return DB_DC_Userdata.ReadAll();
        }
        public override string ToString()
        {
            return $"";
        }
        public static void Add(DC_Userdata ud, bool notification)
        {
            DB_DC_Userdata.Add(ud, notification);
        }
        public static void Change(DC_Userdata ud, bool notification)
        {
            DB_DC_Userdata.Change(ud, notification);
        }
        public static void CreateTable_Userdata(bool notification)
        {
            DB_DC_Userdata.CreateTable_Userdata(notification);
        }
    }
}
