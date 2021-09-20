using BotDLL;
using BotDLL.Model.BotCom;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ListforgeBot
{
    public class Program
    {
        #region Test
        /*
        static void Main()
        {
            DiscordBot bot = new DiscordBot();
            bot.RunAsync().Wait();
        }*/
        #endregion

        #region Variables
        /// <summary>
        /// The ms time out.
        /// </summary>
        private const int MSTimeOut = 5000;
        /// <summary>
        /// The ms time out bt.
        /// </summary>
        private const int MSTimeOutbt = 5000;
        private static List<LF_ServerInfo> lstlive = new List<LF_ServerInfo>();
        private static List<LF_ServerInfo> lstcp1 = new List<LF_ServerInfo>();
        private static List<LF_ServerInfo> lstcp2 = new List<LF_ServerInfo>();
        /// <summary>
        /// The db.
        /// </summary>
        private const string db = "LF_ServerInfoLive";
        private static DiscordBot dbot;
        #endregion

        /// <summary>
        /// The main program.
        /// </summary>
        static async Task Main()
        {
            try
            {
                Console.SetWindowSize(250, 49);
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
                    dbot = new DiscordBot();
                    await dbot.RunAsync();
                    await TelegramBot.Init();
                    DebugLog.Main();
                    LF_Fetcher.Fetch(db); Thread.Sleep(MSTimeOut);
                    lstlive = LF_ServerInfo.ReadAll(db); Thread.Sleep(MSTimeOutbt);
                    CSV_LF_ServerInfo.WriteAll(compare1, lstlive); Thread.Sleep(MSTimeOutbt);
                    CSV_LF_ServerInfo.WriteAll(compare2, lstlive); Thread.Sleep(MSTimeOutbt);
                    lstcp1 = CSV_LF_ServerInfo.ReadALL(compare1); Thread.Sleep(MSTimeOut);
                    virgin++;
                }
                Thread.Sleep(MSTimeOut);
                LF_Fetcher.Fetch(db); Thread.Sleep(MSTimeOut);
                lstlive = LF_ServerInfo.ReadAll(db); Thread.Sleep(MSTimeOutbt);
                CSV_LF_ServerInfo.WriteAll(compare2, lstlive); Thread.Sleep(MSTimeOut);
                lstcp2 = CSV_LF_ServerInfo.ReadALL(compare2); Thread.Sleep(MSTimeOutbt);
                DidChangeQM(lstcp1, lstcp2);
                Console.ForegroundColor = ConsoleColor.Green; WriteList(lstlive); Thread.Sleep(MSTimeOut);
                LF_Fetcher.Fetch(db); Thread.Sleep(MSTimeOut);
                lstlive = LF_ServerInfo.ReadAll(db); Thread.Sleep(MSTimeOutbt);
                CSV_LF_ServerInfo.WriteAll(compare1, lstlive); Thread.Sleep(MSTimeOut);
                lstcp1 = CSV_LF_ServerInfo.ReadALL(compare1); Thread.Sleep(MSTimeOutbt);
                DidChangeQM(lstcp1, lstcp2);
                Console.ForegroundColor = ConsoleColor.Red; WriteList(lstcp2);
                Thread.Sleep(MSTimeOut);
                GC.Collect();
                virgin++;
                if (virgin == 1075)
                    RestartProgram();
            }
        }
        /// <summary>
        /// Checks for changes.
        /// </summary>
        /// <param name="lstv1">The LF server info 1.</param>
        /// <param name="lstv2">The LF server info 1.</param>
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
                                LF_ServerInfo obj = itemlive;
                                TelegramBot.TGChange(obj, "player");
                                DiscordBot.DCChange(obj, "player");
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
                                LF_ServerInfo obj = itemlive;
                                TelegramBot.TGChange(obj, "status");
                                DiscordBot.DCChange(obj, "status");
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
                                LF_ServerInfo obj = itemlive;
                                TelegramBot.TGChange(obj, "version");
                                DiscordBot.DCChange(obj, "version");
                                Center($"{obj}");
                                Change();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Writes the list.
        /// </summary>
        /// <param name="lst">The LF server info.</param>
        static void WriteList(List<LF_ServerInfo> lst)
        {
            try
            {
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                Center($"{"Id",8} ██ {"Name",-15} ██ {"Address",20}:{"Port",-5} ██ {"Hostname",-22} ██ {"Map",-20} ██ {"Online",-5} ██ {"Pl",3}/{"MaxPl",-5} ██ {"Version",10} ██ {"UpT",-3}% ██  {"Last_check",-19}  ██  {"Last_online",-19}");
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

        /// <summary>
        /// Centers the console.
        /// </summary>
        /// <param name="s">The text.</param>
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

        /// <summary>
        /// Change banner.
        /// </summary>
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

        /// <summary>
        /// Restarts the program.
        /// </summary>
        private static void RestartProgram()
        {
            // Get file path of current process 
            var filePath = Assembly.GetExecutingAssembly().Location;
            //var filePath = Application.ExecutablePath;  // for WinForms

            // Start program
            Process.Start(filePath);

            // For all Windows application but typically for Console app.
            Environment.Exit(0);
        }
    }
}
