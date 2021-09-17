using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class LF_Valheim
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Private { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Is_online { get; set; }
        public string Players { get; set; }
        public string Maxplayers { get; set; }
        public string Version { get; set; }
        public object Platform { get; set; }
        public string Uptime { get; set; }
        public string Score { get; set; }
        public string Rank { get; set; }
        public string Votes { get; set; }
        public string Favorited { get; set; }
        public string Comments { get; set; }
        public string Url { get; set; }
        public string Last_check { get; set; }
        public string Last_online { get; set; }
        public static LF_Valheim CreateObj(String content)
        {
            LF_Valheim lst = JsonConvert.DeserializeObject<LF_Valheim>(content);
            LF_Valheim obj = new LF_Valheim
            {
                Id = lst.Id,
                Name = lst.Name,
                Address = lst.Address,
                Port = lst.Port,
                Private = lst.Private,
                Password = lst.Password,
                Location = lst.Location,
                Is_online = lst.Is_online,
                Players = lst.Players,
                Maxplayers = lst.Maxplayers,
                Version = lst.Version,
                Platform = lst.Platform,
                Uptime = lst.Uptime,
                Score = lst.Score,
                Rank = lst.Rank,
                Votes = lst.Votes,
                Favorited = lst.Favorited,
                Comments = lst.Comments,
                Url = lst.Url,
                Last_check = lst.Last_check,
                Last_online = lst.Last_online,
            };
            return obj;
        }
    }
}