using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Okolni.Source.Query;

namespace BotDLL.Model.SourceQuery
{
    public class SourceQuery
    {
        public void QueryConnection()
        {

            string ipaddress = "0x360x39.de";
            IPAddress address = Dns.GetHostAddresses(ipaddress)[0];
            
            IQueryConnection conn = new QueryConnection();
            
            conn.Host = address.ToString(); // IP
            conn.Port = 20350; // Port

            conn.Connect(); // Create the initial connection

            var info = conn.GetInfo(); // Get the Server info
            var players = conn.GetPlayers(); // Get the Player info
            var rules = conn.GetRules(); // Get the Rules
        }
    }
}
