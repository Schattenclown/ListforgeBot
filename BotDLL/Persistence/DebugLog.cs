using System;
using System.IO;
/// <summary>
/// Debug logfile.
/// </summary>

public class DebugLog
{
    private static DateTime date = DateTime.Now;
    private static Uri _path = new Uri($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ListforgeBot/log");
    private static Uri _filepath = new Uri($"{_path}/log-{date.ToString().Replace(':', '-')}.txt");
    private static StreamWriter streamWriter;
    /// <summary>
    /// initialization of the debuglog
    /// </summary>
    public static void Main()
    {
        DirectoryInfo dir = new DirectoryInfo(_path.LocalPath);
        if (!dir.Exists)
            dir.Create();

        streamWriter = File.AppendText(_filepath.LocalPath);
        streamWriter.Write($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}  ");
        streamWriter.WriteLine("Log Entry \r");
        streamWriter.Close();
    }
    /// <summary>
    /// Writes into the logfile
    /// </summary>
    /// <param name="logMessage">The message that gets appendet to the logfile.</param>
    public static void WriteLog(string logMessage)
    {
        date = DateTime.Now;
        streamWriter = File.AppendText(_filepath.LocalPath);
        streamWriter.WriteLine($"{date}  {logMessage}");
        streamWriter.Close();
    }
}