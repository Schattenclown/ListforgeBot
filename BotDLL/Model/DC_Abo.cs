using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    class DC_Abo
    {
        public UInt64 Channel { get; set; }
        public string Server { get; set; }
        public DC_Abo(UInt64 channel, string server)
        {
            this.Channel = channel;
            this.Server = server;
        }
    }
}
