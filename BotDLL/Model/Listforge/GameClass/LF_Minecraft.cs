using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class LF_Minecraft
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Banner_url { get; set; }
        public string Location { get; set; }
        public string Is_online { get; set; }
        public object Is_private { get; set; }
        public string Theme { get; set; }
        public string Players { get; set; }
        public string Maxplayers { get; set; }
        public string Version { get; set; }
        public string Uptime { get; set; }
        public string Score { get; set; }
        public string Rank { get; set; }
        public string Votes { get; set; }
        public string Favorited { get; set; }
        public string Comments { get; set; }
        public string Url { get; set; }
        public string Last_check { get; set; }
        public string Last_online { get; set; }
        public string Registration_date { get; set; }
        public string Update_date { get; set; }
        public static LF_Minecraft CreateObj(String content)
        {
            LF_Minecraft lst = JsonConvert.DeserializeObject<LF_Minecraft>(content);
            LF_Minecraft obj = new LF_Minecraft
            {
                Id = lst.Id,
                Name = lst.Name,
                Address = lst.Address,
                Port = lst.Port,
                Banner_url = lst.Banner_url,
                Location = lst.Location,
                Is_online = lst.Is_online,
                Is_private = lst.Is_private,
                Theme = lst.Theme,
                Players = lst.Players,
                Maxplayers = lst.Maxplayers,
                Version = lst.Version,
                Uptime = lst.Uptime,
                Score = lst.Score,
                Rank = lst.Rank,
                Votes = lst.Votes,
                Favorited = lst.Favorited,
                Comments = lst.Comments,
                Url = lst.Url,
                Last_check = lst.Last_check,
                Last_online = lst.Last_online,
                Registration_date = lst.Registration_date,
                Update_date = lst.Update_date
            };
            return obj;
        }
    }
}

