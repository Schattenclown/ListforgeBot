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
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
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
        private static DiscordBot dBot;
        #endregion

        /// <summary>
        /// The main program.
        /// </summary>
        static async Task Main()
        {
            DebugLog.Main();

            try
            {
                try
                {
                    Console.SetWindowSize(250, 49);
                }
                catch (Exception)
                {
                    Console.SetWindowSize(100, 10);
                    DebugLog.WriteLog("ERROR: Screenresolution was to small for the Console");
                }
                int virgin = 0;
                string compare1 = "LFServerInfocompare1";
                string compare2 = "LFServerInfocompare2";

                while (true)
                {
                    if (virgin == 0)
                    {
                        DebugLog.WriteLog("DEBUG: Entered virgin state");
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
                        dBot = new DiscordBot();
                        await dBot.RunAsync();
                        await TelegramBot.Init();
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
                    //1075 about 24h
                    if (virgin == 1075)
                    {
                        Program p = new Program();
                        await p.RestartProgram();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("something");
                Program p = new Program();
                await p.RestartProgram();
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
                                Change(obj);
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
                                Change(obj);
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
                                Change(obj);
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
                Center($"{"Id",8} ██ {"Name",-15} ██ {"Address",20}:{"Port",-5} ██ {"Hostname",-30} ██ {"Map",-20} ██ {"Online",-5} ██ {"Pl",3}/{"MaxPl",-5} ██ {"Version",10} ██ {"UpT",-3}% ██  {"Last_check",-19}  ██  {"Last_online",-19}");
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
                s = "Console to smoll EXC";
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.Write(s);
                Console.SetCursorPosition((Console.WindowWidth - 4), Console.CursorTop);
                Console.WriteLine("██");
            }
        }

        /// <summary>
        /// Change banner.
        /// </summary>
        static void Change(LF_ServerInfo obj)
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
                DebugLog.WriteLog($"{obj.Name} {obj.Players} {obj.Address}:{obj.Port} {obj.LF_Uri.AbsoluteUri}");
            }
            catch (Exception)
            {
                Center("Console to smoll for CHANGE");
            }
        }

        /// <summary>
        /// Restarts the program.
        /// </summary>
        private async Task RestartProgram()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
            Center(" ");
            Center(@"██████╗ ███████╗███████╗████████╗ █████╗ ██████╗ ████████╗██╗███╗   ██╗ ██████╗ ");
            Center(@"██╔══██╗██╔════╝██╔════╝╚══██╔══╝██╔══██╗██╔══██╗╚══██╔══╝██║████╗  ██║██╔════╝ ");
            Center(@"██████╔╝█████╗  ███████╗   ██║   ███████║██████╔╝   ██║   ██║██╔██╗ ██║██║  ███╗");
            Center(@"██╔══██╗██╔══╝  ╚════██║   ██║   ██╔══██║██╔══██╗   ██║   ██║██║╚██╗██║██║   ██║");
            Center(@"██║  ██║███████╗███████║   ██║   ██║  ██║██║  ██║   ██║   ██║██║ ╚████║╚██████╔╝");
            Center(@"╚═╝  ╚═╝╚══════╝╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ");
            Center(" ");
            Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
            Center("Restating in 10 secounds.");
            Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
            await Task.Delay(1000 * 10);

            dBot.Dispose();
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
