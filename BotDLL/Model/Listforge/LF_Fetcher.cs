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
    public class LF_Fetcher
    {
        public static async void Fetch(string db)
        {
            List<LF_API_Uri> lst = LF_API_Uri.ReadAll();

            foreach (var item in lst)
            {
                bool achieved = false;
                var client = new HttpClient();
                var client1 = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                //522 origin trycatch? auch bei con int 
                try
                {
                    string content = await client.GetStringAsync($"{item.Combined}");

                    while (achieved == false)
                    {
                        if (content.Contains("minecraft-mp.com"))
                        {
                            LF_Minecraft obj = LF_Minecraft.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("minecraft-mp.com", obj.Id);
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, "", "", obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, false);
                        }
                        else if (content.Contains("ark-servers.net"))
                        {
                            LF_ARK obj = LF_ARK.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("ark-servers.net", obj.Id);
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, obj.Map, obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, false);
                        }
                        else if (content.Contains("conan-exiles.com"))
                        {
                            LF_Conan obj = LF_Conan.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("conan-exiles.com", obj.Id);
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, obj.Map, obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, false);
                        }
                        else if (content.Contains("teamspeak-servers.org"))
                        {
                            LF_TeamSpeak obj = LF_TeamSpeak.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("teamspeak-servers.org", obj.Id);
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, "", obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, false);
                        }
                        else if (content.Contains("counter-strike-servers.net"))
                        {
                            LF_CSGO obj = LF_CSGO.CreateObj(content);
                            DateTime last_check = Builddatetime(obj.Last_check);
                            DateTime last_online = Builddatetime(obj.Last_online);
                            string LF_StatUri = BuildStatUrl("counter-strike-servers.net", obj.Id);
                            string rawcontent = await client1.GetStringAsync(LF_StatUri);
                            QC_UriGenerator qcobj = QC_UriGenerator.CreateObj(rawcontent);
                            achieved = DB_LF_ServerInfo.InserInto(db, obj.Id, obj.Name, obj.Address, obj.Port, obj.Hostname, obj.Map, obj.Is_online, obj.Players, obj.Maxplayers, obj.Version, obj.Uptime, last_check, last_online, obj.Url, item.Key, LF_StatUri, qcobj.QC_StatUri, false);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
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
                default:
                    break;
            }
            return finishedUrl;
        }
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
            DateTime dt = new DateTime(year, imonth, day, hour, minute, 0).AddHours(7);
            if (itspm)
                dt.AddHours(12);
            return dt;
        }
    }
}
