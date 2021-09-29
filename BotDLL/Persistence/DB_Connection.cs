using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BotDLL.HelpClasses;

namespace BotDLL
{
    class DB_Connection
    {
        private static string token = "";
        private static int virgin = 0;
        public static void SetDB()
        {
            Connections connections = Connections.GetConnections();
            token = connections.MySqlConStr;
#if DEBUG
            token = connections.MySqlConStrDebug;
#endif
        }
        public static MySqlConnection OpenDB()
        {
            if (virgin == 0)
                SetDB(); virgin = 69;
            MySqlConnection connection = new MySqlConnection(token);
            do
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    DebugLog.WriteLog("ERROR: DB_Connection.OpenDB" + ex.Message);
                    Thread.Sleep(10);
                }
            } while (connection == null);

            return connection;
        }
        public static void CloseDB(MySqlConnection connection)
        {
            connection.Close();
        }
        public static void ExecuteNonQuery(String sql, bool showMessageBox)
        {
            MySqlConnection connection = OpenDB();
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            int ret = sqlCommand.ExecuteNonQuery();
            if (ret != -1 && showMessageBox == true)
                MessageBox.Show("Worked!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseDB(connection);
        }
        public static MySqlDataReader ExecuteReader(String sql, MySqlConnection connection)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            try
            {
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                return sqlDataReader;
            }
            catch (Exception)
            {
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                Center("DB IS DEAD");
                Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");
                RestartProgram();
                throw new Exception("DB DeaD");
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
        /// Restarts the program.
        /// </summary>
        private static void RestartProgram()
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
            Center("DB IS DEAD");
            Console.WriteLine($"{"".PadRight(Console.WindowWidth - 2, '█')}");

            // Get file path of current process 
            var filePath = Assembly.GetExecutingAssembly().Location;
            var newFilepath = "";
            //BotDLL.dll
            
            if (filePath.Contains("Debug"))
            {
                filePath = WordCutter.RemoveAfterWord(filePath, "Debug", 0);
                newFilepath = filePath + "Debug\\ListforgeBot.exe";
            }
            else if (filePath.Contains("Release"))
            {
                filePath = WordCutter.RemoveAfterWord(filePath, "Release", 0);
                newFilepath = filePath + "Release\\ListforgeBot.exe";
            }
            Console.WriteLine("Before 120 secound sleep");
            Thread.Sleep(1000 * 60);
            Console.WriteLine("After 120 secound sleep");
            // Start program
            Process.Start(newFilepath);

            // For all Windows application but typically for Console app.
            Environment.Exit(0);
        }
    }
}
