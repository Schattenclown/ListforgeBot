using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class TL_Userdata
    {
        public int UserNr { get; set; }
        public int ChatId { get; set; }
        public String Username { get; set; }
        public int ServerId { get; set; }
        public bool Abo { get; set; }

        public static List<TL_Userdata> ReadAll()
        {
            return DB_TL_Userdata.ReadAll();
        }
        public override string ToString()
        {
            return $"";
        }
        public static void Add(TL_Userdata ud, bool notification)
        {
            DB_TL_Userdata.Add(ud, notification);
        }
        public static void Change(TL_Userdata ud, bool notification)
        {
            DB_TL_Userdata.Change(ud, notification);
        }
        public static void CreateTable_Userdata(bool notification)
        {
            DB_TL_Userdata.CreateTable_Userdata(notification);
        }
    }
}
