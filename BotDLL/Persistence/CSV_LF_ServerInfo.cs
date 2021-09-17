using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class CSV_LF_ServerInfo
    {
        private static string _compare;
        private static Uri _path;
        public static void WriteAll(string compare, List<LF_ServerInfo> lst) 
        {
            _compare = compare;
            _path = new Uri($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ListforgeBot/{_compare}.csv");
            while (true)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(_path.LocalPath, false);
                    foreach (var item in lst)
                    {
                        sw.WriteLine($"{item.Id};{item.Name};{item.Address};{item.Port};{item.Hostname};{item.Map};{item.Is_online};{item.Players};{item.Maxplayers};{item.Version};{item.Uptime};{item.LF_Uri.AbsoluteUri};{item.Last_check};{item.Last_online}");
                    }
                    sw.Close();
                    break;
                }
                catch (Exception)
                {
                    DebugLog.Log("Error: Failed Insert in CSV_LF_ServerInfo.WriteAll");
                }
            }
        }
        public static List<LF_ServerInfo> ReadALL(string compare)
        {
            _compare = compare;
            _path = new Uri($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ListforgeBot/{_compare}.csv");
            while (true)
            {
                try
                {
                    List<LF_ServerInfo> lst = new List<LF_ServerInfo>();
                    StreamReader sr = new StreamReader(_path.LocalPath);
                    while (!sr.EndOfStream)
                    {
                        string zeile = sr.ReadLine();
                        String[] arr = zeile.Split(';');
                        LF_ServerInfo obj = new LF_ServerInfo
                        {
                            Id = Convert.ToInt32(arr[0]),
                            Name = arr[1],
                            Address = arr[2],
                            Port = Convert.ToInt32(arr[3]),
                            Hostname = arr[4],
                            Map = arr[5],
                            Is_online = Convert.ToBoolean(arr[6]),
                            Players = Convert.ToInt32(arr[7]),
                            Maxplayers = Convert.ToInt32(arr[8]),
                            Version = arr[9],
                            Uptime = Convert.ToInt32(arr[10]),
                            LF_Uri = new Uri(arr[11]),
                            Last_check = Convert.ToDateTime(arr[12]),
                            Last_online = Convert.ToDateTime(arr[13])
                        };
                        lst.Add(obj);
                    }
                    return lst;
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
