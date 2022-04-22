using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

public static class Logger
{
    private static ConcurrentQueue<LogPackage> Queue = new();

    static Logger()
    {
        try
        {
            Console.BufferWidth = Console.WindowWidth;
            ClearBuffer();
        }
        catch { Console.WriteLine("[CRITICAL]: Logger is unable to initialise!"); }
        Task.Run(() => 
        {
            while (true)
            {
                if (!Queue.IsEmpty && Queue.TryDequeue(out LogPackage result)) PushLog(result);
            }
        });
    }

    private static void PushLog(LogPackage pckg)
    {
        if (pckg.ClearMode is 0)
        {
            if (pckg.Level is -1) Console.Write(pckg.Message);
            else if (pckg.Level is 0) Console.WriteLine(pckg.Message);
            else if (pckg.Level is 1) Console.WriteLine(pckg.Message);
            else if (pckg.Level is 2) Console.Error.WriteLine(pckg.Message);
            else if (pckg.Level is 3) Debug.WriteLine(pckg.Message);
        }
        else if (pckg.ClearMode is 3) Console.Clear();
        else if (pckg.ClearMode is 2) Console.WriteLine();
        else if (pckg.ClearMode is 1) Console.WriteLine(pckg.Message);
    }

    public static void Log(object msg)
    {
        LogPackage pckg = default;
        pckg.Level = -1;
        pckg.Message = msg is not null ? $"{msg}" : "null";
        PushLog(pckg);
    }

    public static void LogInfo(object msg)
    {
        LogPackage pckg = default;
        pckg.PostTime = DateTime.Now;
        pckg.Level = 0;
        pckg.Message = msg is not null ? $"[INFO] {msg}" : "null";
        PushLog(pckg);
    }

    public static void LogWarn(object msg)
    {
        LogPackage pckg = default;
        pckg.PostTime = DateTime.Now;
        pckg.Level = 1;
        pckg.Message = msg is not null ? $"[WARN] {msg}" : "null";
        PushLog(pckg);
    }

    public static void LogError(object msg)
    {
        LogPackage pckg = default;
        pckg.PostTime = DateTime.Now;
        pckg.Level = 2;
        pckg.Message = msg is not null ? $"[ERR]  {msg}" : "null";
        PushLog(pckg);
    }

#if DEBUG
    public static void LogDebug(object msg)
    {
        LogPackage pckg = default;
        pckg.PostTime = DateTime.Now;
        pckg.Level = 3;
        pckg.Message = msg is not null ? $"[DBG]  {msg}" : "null";
        PushLog(pckg);
    }
#else
    public static void LogDebug(object msg)
    {
        LogPackage pckg = default;
        pckg.PostTime = DateTime.Now;
        pckg.Level = 3;
        pckg.Message = "Debug logs should not be called in Release builds!";
        PushLog(pckg);
    }
#endif

    public static void LogException<T>(T e) where T : Exception
    {
        LogPackage pckg = default;
        pckg.PostTime = DateTime.Now;
        pckg.Level = 2;
        pckg.Message = $"[EXCE] Source: {e.Source}\n | Data: {e.Data}\n | Message: {e.Message}\n | StackTrace: {e.StackTrace}";
        PushLog(pckg);
    }

    public static void NewLine(int lines = 1)
    {
        if (lines < 1) lines = 1;
        for (int i = 0; i < lines; i++)
        {
            LogPackage pckg = default;
            pckg.ClearMode = 2;
            PushLog(pckg);
        }
    }

    public static void DivideBuffer()
    {
        StringBuilder b = new();
        for (int i = 0; i < Console.BufferWidth - 1; i++) b.Append('-');
        LogPackage pckg = default;
        pckg.ClearMode = 1;
        pckg.Message = b.ToString();
        PushLog(pckg);
    }

    public static void ClearLine(string? content = null)
    {
        StringBuilder b = new(content is null ? string.Empty : content);
        for (int i = 0; i < Console.BufferWidth - 1; i++) b.Append(' ');
        Console.Write("\r{0}", b.ToString());
    }

    public static void ClearBuffer()
    {
        LogPackage pckg = default;
        pckg.ClearMode = 3;
        PushLog(pckg);
    }

    private struct LogPackage
    {
        internal DateTime PostTime { get; set; }
        internal int ClearMode { get; set; }
        internal int Level { get; set; }
        internal string Message { get; set; }
    }
}
