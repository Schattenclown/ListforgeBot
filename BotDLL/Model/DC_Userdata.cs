using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class DC_Userdata
    {
        public string AuthorId { get; set; }
        public string ChannelId { get; set; }
        public string Author { get; set; }
        public int ServerId { get; set; }
        public bool Abo { get; set; }

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
