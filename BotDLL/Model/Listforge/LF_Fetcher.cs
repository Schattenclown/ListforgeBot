using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Threading;

namespace BotDLL
{
    /// <summary>
    /// The listforge fetcher.
    /// </summary>
    public class LF_Fetcher
    {
        private static Random Random = new Random();
        private static int IRandom;
        /// <summary>
        /// Fetches the data.
        /// </summary>
        /// <param name="db">The db.</param>
        public static async void Fetch(string db)
        {
            List<LF_API_Uri> lst = LF_API_Uri.ReadAll();

            foreach (var item in lst)
            {
                bool achieved = false;
                var client = new HttpClient();
                var client1 = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                try
                {
                    string content = await client.GetStringAsync($"{item.Combined}");
                    if(content.Contains("server key not found"))
                    {
                        DebugLog.WriteLog($"server key not found for {item.Combined}");
                        DB_LF_ServerInfo.Delete(item, false);
                        DB_LF_API_Uri.Delete(item, false);
                        return;
                    }

                    while (achieved == false)
                    {
                        if (content.Contains("minecraft-mp.com"))
                        {
                            LF_Minecraft obj = LF_Minecraft.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("minecraft-mp.com", obj.Id);
                            Uri LF_HeaderImgUri = new Uri(BuildHeaderImgUri("minecraft-mp.com", obj.Id, null));
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, "", "", obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, LF_HeaderImgUri, false);
                        }
                        else if (content.Contains("ark-servers.net"))
                        {
                            LF_ARK obj = LF_ARK.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("ark-servers.net", obj.Id);
                            Uri LF_HeaderImgUri = new Uri(BuildHeaderImgUri("ark-servers.net", obj.Id, obj.Map));
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, obj.Map, obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, LF_HeaderImgUri, false);
                        }
                        else if (content.Contains("conan-exiles.com"))
                        {
                            LF_Conan obj = LF_Conan.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            
                            string LF_StatUri = BuildStatUrl("conan-exiles.com", obj.Id);

                            Uri LF_HeaderImgUri = new Uri(BuildHeaderImgUri("conan-exiles.com", obj.Id, obj.Map));
                            if (obj.Map.ToString() == "Normal")
                                LF_HeaderImgUri = null;

                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);

                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, obj.Map, obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, LF_HeaderImgUri, false);
                        }
                        else if (content.Contains("teamspeak-servers.org"))
                        {
                            LF_TeamSpeak obj = LF_TeamSpeak.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("teamspeak-servers.org", obj.Id);
                            Uri LF_HeaderImgUri = new Uri(BuildHeaderImgUri("teamspeak-servers.org", obj.Id, null));
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, "", obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, LF_HeaderImgUri, false);
                        }
                        else if (content.Contains("counter-strike-servers.net"))
                        {
                            LF_CSGO obj = LF_CSGO.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("counter-strike-servers.net", obj.Id);
                            Uri LF_HeaderImgUri = new Uri(BuildHeaderImgUri("counter-strike-servers.net", obj.Id, obj.Map));
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, obj.Map, obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, LF_HeaderImgUri, false);
                        }
                        else if (content.Contains("valheim-servers.io"))
                        {
                            LF_Valheim obj = LF_Valheim.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("valheim-servers.io", obj.Id);
                            Uri LF_HeaderImgUri = new Uri(BuildHeaderImgUri("valheim-servers.io", obj.Id, null));
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, null, null, obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, LF_HeaderImgUri, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DebugLog.WriteLog("ERROR: LF_Fetcher.Fetch " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Builds the stat url.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="id">The id.</param>
        /// <returns>A string.</returns>
        static string BuildStatUrl(string game, string id)
        {
            string finishedUrl = "";
            switch (game)
            {
                case "minecraft-mp.com":
                    finishedUrl = $"https://minecraft-mp.com/statistics/chart/daily/players/{id}/";
                    break;
                case "ark-servers.net":
                    finishedUrl = $"https://ark-servers.net/statistics/chart/daily/players/{id}/";
                    break;
                case "conan-exiles.com":
                    finishedUrl = $"https://conan-exiles.com/statistics/chart/daily/players/{id}/";
                    break;
                case "teamspeak-servers.org":
                    finishedUrl = $"https://teamspeak-servers.org/statistics/chart/daily/players/{id}/";
                    break;
                case "counter-strike-servers.net":
                    finishedUrl = $"https://counter-strike-servers.net/statistics/chart/daily/players/{id}/";
                    break;
                case "valheim-servers.io":
                    finishedUrl = $"https://valheim-servers.io/statistics/chart/daily/players/{id}/";
                    break;
                default:
                    break;
            }
            return finishedUrl;
        }
        /// <summary>
        /// Builds the header img uri.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="id">The id.</param>
        /// <param name="map">The map.</param>
        /// <returns>A string.</returns>
        static string BuildHeaderImgUri(string game, string id, string map)
        {
            string finishedUrl = "";
            switch (game)
            {
                case "minecraft-mp.com":

                    IRandom = Random.Next(1, 7);
                    switch (IRandom)
                    {
                        case 1:
                            finishedUrl = $"https://minecraft-mp.com/banner-{id}.png";
                            break;
                        case 2:
                            finishedUrl = $"https://minecraft-mp.com/banner-{id}-2.png";
                            break;
                        case 3:
                            finishedUrl = $"https://minecraft-mp.com/banner-{id}-3.png";
                            break;
                        case 4:
                            finishedUrl = $"https://minecraft-mp.com/banner-{id}-4.png";
                            break;
                        case 5:
                            finishedUrl = $"https://minecraft-mp.com/banner-{id}-5.png";
                            break;
                        case 6:
                            finishedUrl = $"https://minecraft-mp.com/banner-{id}-6.png";
                            break;
                    }
                    break;

                case "ark-servers.net":

                    IRandom = Random.Next(1, 4);
                    switch (IRandom)
                    {
                        case 1:
                            finishedUrl = $"https://ark-servers.net/server/{id}/banners/banner-1.png";
                            break;
                        case 2:
                            finishedUrl = $"https://ark-servers.net/server/{id}/banners/banner-2.png";
                            break;
                        case 3:
                            finishedUrl = $"https://ark-servers.net/server/{id}/banners/banner-3.png";
                            break;
                    }
                    break;

                case "conan-exiles.com":

                    IRandom = Random.Next(1, 4);
                    switch (IRandom)
                    {
                        case 1:
                            finishedUrl = $"https://conan-exiles.com/server/{id}/banners/banner-1.png";
                            break;
                        case 2:
                            finishedUrl = $"https://conan-exiles.com/server/{id}/banners/banner-2.png";
                            break;
                        case 3:
                            finishedUrl = $"https://conan-exiles.com/server/{id}/banners/banner-3.png";
                            break;
                    }
                    break;

                case "teamspeak-servers.org":
                    IRandom = Random.Next(1, 4);
                    switch (IRandom)
                    {
                        case 1:
                            finishedUrl = $"https://teamspeak-servers.org/server/{id}/banners/banner-1.png";
                            break;
                        case 2:
                            finishedUrl = $"https://teamspeak-servers.org/server/{id}/banners/banner-2.png";
                            break;
                        case 3:
                            finishedUrl = $"https://teamspeak-servers.org/server/{id}/banners/banner-3.png";
                            break;
                    }
                    break;

                case "counter-strike-servers.net":
                    finishedUrl = $"https://counter-strike-servers.net/server/{id}/banners/banner-1.png";
                    break;

                case "valheim-servers.io":
                    IRandom = Random.Next(1, 4);
                    switch (IRandom)
                    {
                        case 1:
                            finishedUrl = $"https://valheim-servers.io/server/{id}/banners/banner-1.png";
                            break;
                        case 2:
                            finishedUrl = $"https://valheim-servers.io/server/{id}/banners/banner-2.png";
                            break;
                        case 3:
                            finishedUrl = $"https://valheim-servers.io/server/{id}/banners/banner-3.png";
                            break;
                    }
                    break;

                default:
                    break;
            }
            return finishedUrl;
        }
        /// <summary>
        /// Build the datetime.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>A DateTime.</returns>
        static DateTime Builddatetime(String datetime)
        {
            bool itspm = false;
            datetime = datetime.Replace("th,", "").Replace("st,", "").Replace("nd,", "").Replace("rd,", "");
            String[] adt = datetime.Split(' ', ':');
            string smonth = adt[0];
            int day = Convert.ToInt32(adt[1]);
            int year = Convert.ToInt32(adt[2]);
            int hour = Convert.ToInt32(adt[3]);
            if (adt[5] == "PM")
                itspm = true;
            int minute = Convert.ToInt32(adt[4]);

            int imonth;
            switch (smonth)
            {
                case "January":
                    imonth = 1;
                    break;
                case "February":
                    imonth = 2;
                    break;
                case "March":
                    imonth = 3;
                    break;
                case "April":
                    imonth = 4;
                    break;
                case "May":
                    imonth = 5;
                    break;
                case "June":
                    imonth = 6;
                    break;
                case "July":
                    imonth = 7;
                    break;
                case "August":
                    imonth = 8;
                    break;
                case "September":
                    imonth = 9;
                    break;
                case "October":
                    imonth = 10;
                    break;
                case "November":
                    imonth = 11;
                    break;
                case "December":
                    imonth = 12;
                    break;
                default:
                    imonth = 1;
                    break;
            }

            if (!itspm && hour == 12)
                hour = 0;

            DateTime dt = new DateTime(year, imonth, day, hour, minute, 0);

            if (itspm && hour != 0 && hour != 12)
                dt = dt.AddHours(12);

            dt = dt.AddHours(7);

            return dt;
        }
    }
}
