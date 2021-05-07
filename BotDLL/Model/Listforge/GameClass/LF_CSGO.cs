using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    class LF_CSGO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Private { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public string Map { get; set; }
        public string Is_online { get; set; }
        public string Players { get; set; }
        public string Maxplayers { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string Uptime { get; set; }
        public string Score { get; set; }
        public string Rank { get; set; }
        public string Votes { get; set; }
        public string Favorited { get; set; }
        public string Comments { get; set; }
        public string Url { get; set; }
        public string Last_check { get; set; }
        public string Last_online { get; set; }
        public static LF_CSGO CreateObj(String content)
        {
            LF_CSGO lst = JsonConvert.DeserializeObject<LF_CSGO>(content);
            LF_CSGO obj = new LF_CSGO
            {
                Id = lst.Id,
                Name = lst.Name,
                Address = lst.Address,
                Port = lst.Port,
                Private = lst.Private,
                Password = lst.Password,
                Location = lst.Location,
                Hostname = lst.Hostname,
                Map = lst.Map,
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
