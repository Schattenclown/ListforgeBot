using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    class DC_Abo
    {
        public UInt64 ChannelId { get; set; }
        public UInt64 AutorId { get; set; }
        public string Server { get; set; }
        public DC_Abo(UInt64 channelId, UInt64 autorId, string server)
        {
            this.ChannelId = channelId;
            this.AutorId = autorId;
            this.Server = server;
        }
    }
}
