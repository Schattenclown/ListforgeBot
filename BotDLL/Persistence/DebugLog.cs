using System;
using System.IO;

public class DebugLog
{
    private static DateTime date = DateTime.Now;
    private static Uri _path = new Uri($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/ListforgeBot/log");
    private static Uri _filepath = new Uri($"{_path}/log-{date.ToString().Replace(':', '-')}.txt");
    private static StreamWriter streamWriter;
    public static void Main()
    {
        DirectoryInfo dir = new DirectoryInfo(_path.LocalPath);
        if (!dir.Exists)
            dir.Create();

        streamWriter = File.AppendText(_filepath.LocalPath);
        streamWriter.Write("\r\nLog Entry : ");
        streamWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
        streamWriter.Close();
    }
    public static void Log(string logMessage)
    {
        date = DateTime.Now;
        streamWriter = File.AppendText(_filepath.LocalPath);
        streamWriter.WriteLine($"{date} {logMessage}\n");
        streamWriter.Close();
    }
}