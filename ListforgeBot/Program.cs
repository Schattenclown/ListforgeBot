using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BotDLL;

namespace ListforgeBot
{
    public class Program
    {
        private const int MSTimeOut = 5000;
        private const int MSTimeOutbt = 5000;
        private static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        private static List<LF_ServerInfo> lstcp1 = new List<LF_ServerInfo>();
        private static List<LF_ServerInfo> lstcp2 = new List<LF_ServerInfo>();
        private const string db = "LF_ServerInfoLive";
        static void Main()
        {
            try
            {
                Console.SetWindowSize(200, 49);
            }
            catch (Exception)
            {
                Console.SetWindowSize(150, 49);
            }
            int virgin = 0;
            string compare1 = "LFServerInfocompare1";
            string compare2 = "LFServerInfocompare2";

            while (true)
            {
                if (virgin == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                    Center(" ");
                    Center(@"███████╗████████╗ █████╗ ██████╗ ████████╗     █████╗ ██╗   ██╗████████╗ ██████╗     ███╗   ██╗ ██████╗ ████████╗██╗███████╗██╗   ██╗");
                    Center(@"██╔════╝╚══██╔══╝██╔══██╗██╔══██╗╚══██╔══╝    ██╔══██╗██║   ██║╚══██╔══╝██╔═══██╗    ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝╚██╗ ██╔╝");
                    Center(@"███████╗   ██║   ███████║██████╔╝   ██║       ███████║██║   ██║   ██║   ██║   ██║    ██╔██╗ ██║██║   ██║   ██║   ██║█████╗   ╚████╔╝ ");
                    Center(@"╚════██║   ██║   ██╔══██║██╔══██╗   ██║       ██╔══██║██║   ██║   ██║   ██║   ██║    ██║╚██╗██║██║   ██║   ██║   ██║██╔══╝    ╚██╔╝  ");
                    Center(@"███████║   ██║   ██║  ██║██║  ██║   ██║       ██║  ██║╚██████╔╝   ██║   ╚██████╔╝    ██║ ╚████║╚██████╔╝   ██║   ██║██║        ██║   ");
                    Center(@"╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝       ╚═╝  ╚═╝ ╚═════╝    ╚═╝    ╚═════╝     ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝╚═╝        ╚═╝   ");
                    Center(" ");
                    Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                    DiscordBot.Main();
                    TelegramBot.Main();
                    //LFServerInfo.DeleteAll(); Thread.Sleep(MSTimeOutbt); 
                    LF_Fetcher.Fetch(db); Thread.Sleep(MSTimeOut);
                    lstlive = LF_ServerInfo.ReadAll(db); Thread.Sleep(MSTimeOutbt);
                    CSV_LF_ServerInfo.WriteAll(compare1, lstlive); Thread.Sleep(MSTimeOutbt);
                    CSV_LF_ServerInfo.WriteAll(compare2, lstlive); Thread.Sleep(MSTimeOutbt);
                    lstcp1 = CSV_LF_ServerInfo.ReadALL(compare1); Thread.Sleep(MSTimeOut);
                    virgin++;
                }
                Thread.Sleep(MSTimeOut);
                //LFServerInfo.DeleteAll(); Thread.Sleep(MSTimeOutbt); 
                LF_Fetcher.Fetch(db); Thread.Sleep(MSTimeOut);
                lstlive = LF_ServerInfo.ReadAll(db); Thread.Sleep(MSTimeOutbt);
                CSV_LF_ServerInfo.WriteAll(compare2, lstlive); Thread.Sleep(MSTimeOut);
                lstcp2 = CSV_LF_ServerInfo.ReadALL(compare2); Thread.Sleep(MSTimeOutbt);
                DidChangeQM(lstcp1, lstcp2);
                Console.ForegroundColor = ConsoleColor.Green; WriteList(lstlive); Thread.Sleep(MSTimeOut);
                //LFServerInfo.DeleteAll(); Thread.Sleep(MSTimeOutbt); 
                LF_Fetcher.Fetch(db); Thread.Sleep(MSTimeOut);
                lstlive = LF_ServerInfo.ReadAll(db); Thread.Sleep(MSTimeOutbt);
                CSV_LF_ServerInfo.WriteAll(compare1, lstlive); Thread.Sleep(MSTimeOut);
                lstcp1 = CSV_LF_ServerInfo.ReadALL(compare1); Thread.Sleep(MSTimeOutbt);
                DidChangeQM(lstcp1, lstcp2);
                Console.ForegroundColor = ConsoleColor.Red; WriteList(lstcp2);
                Thread.Sleep(MSTimeOut);
                GC.Collect();
            }
        }
        static void DidChangeQM(List<LF_ServerInfo> lstv1, List<LF_ServerInfo> lstv2)
        {
            foreach (var itemcp1 in lstv1)
            {
                foreach (var itemcp2 in lstv2)
                {
                    if (itemcp1.Id == itemcp2.Id && itemcp1.Players != itemcp2.Players)
                    {
                        lstlive = LF_ServerInfo.ReadAll(db);

                        foreach (var itemlive in lstlive)
                        {
                            if (itemlive.Id == itemcp1.Id)
                            {
                                LF_ServerInfo obj = itemlive as LF_ServerInfo;
                                TelegramBot.DidChangePlayerCount(obj);
                                Center($"{obj}");
                                Change();
                            }
                        }
                    }
                    else if (itemcp1.Id == itemcp2.Id && itemcp1.Is_online != itemcp2.Is_online)
                    {
                        lstlive = LF_ServerInfo.ReadAll(db);

                        foreach (var itemlive in lstlive)
                        {
                            if (itemlive.Id == itemcp1.Id)
                            {
                                LF_ServerInfo obj = itemlive as LF_ServerInfo;
                                TelegramBot.DidChangeStatus(obj);
                                Center($"{obj}");
                                Change();
                            }
                        }
                    }
                    else if (itemcp1.Id == itemcp2.Id && itemcp1.Version != itemcp2.Version)
                    {
                        lstlive = LF_ServerInfo.ReadAll(db);

                        foreach (var itemlive in lstlive)
                        {
                            if (itemlive.Id == itemcp1.Id)
                            {
                                LF_ServerInfo obj = itemlive as LF_ServerInfo;
                                TelegramBot.DidChangePlayerCount(obj);
                                Center($"{obj}");
                                Change();
                            }
                        }
                    }
                }
            }
        }
        static void WriteList(List<LF_ServerInfo> lst)
        {
            try
            {
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                Center($"{"Id",6} ██ {"Name",-15} ██ {"Address",15}:{"Port",-5} ██ {"Hostname",-22} ██ {"Map",-20} ██ {"Online",-5} ██ {"Pl",3}/{"MaxPl",-5} ██ {"Version",10} ██ {"UpT",-3}% ██  {"Last_check",-19}  ██  {"Last_online",-19}");
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                foreach (var item in lst)
                {
                    Center(item.ToString());
                }
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
            }
            catch (Exception)
            {
                Center("Console to smoll for List");
            }
        }
        static void Center(string s)
        {
            try
            {
                Console.Write("██");
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.Write(s);
                Console.SetCursorPosition((Console.WindowWidth - 4), Console.CursorTop);
                Console.WriteLine("██");
            }
            catch (Exception)
            {
                s ="Console to smoll EXC";
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.Write(s);
                Console.SetCursorPosition((Console.WindowWidth - 4), Console.CursorTop);
                Console.WriteLine("██");
            }
        }
        static void Change()
        {
            try
            {
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                Center(" ");
                Center(@" ██████╗██╗  ██╗ █████╗ ███╗   ██╗ ██████╗ ███████╗ ");
                Center(@"██╔════╝██║  ██║██╔══██╗████╗  ██║██╔════╝ ██╔════╝");
                Center(@"██║     ███████║███████║██╔██╗ ██║██║  ███╗█████╗  ");
                Center(@"██║     ██╔══██║██╔══██║██║╚██╗██║██║   ██║██╔══╝  ");
                Center(@"╚██████╗██║  ██║██║  ██║██║ ╚████║╚██████╔╝███████╗");
                Center(@"  ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ");
                Center(" ");
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
            }
            catch (Exception)
            {
                Center("Console to smoll for CHANGE");
            }
        }
    }
}
