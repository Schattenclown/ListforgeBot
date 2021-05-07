using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class TLG_Userdata
    {
        public int UserNr { get; set; }
        public int ChatId { get; set; }
        public String Username { get; set; }
        public int ServerId { get; set; }
        public bool Abo { get; set; }

        public static List<TLG_Userdata> ReadAll()
        {
            return DB_TL_Userdata.ReadAll();
        }
        public override string ToString()
        {
            return $"";
        }
        public static void Add(TLG_Userdata ud)
        {
            DB_TL_Userdata.Add(ud);
        }
        public static void Change(TLG_Userdata ud)
        {
            DB_TL_Userdata.Change(ud);
        }
    }
}
