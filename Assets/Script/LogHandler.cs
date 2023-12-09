using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

public class LogHandler
{
    public static string Error = @"Error";
    public static string Warning = @"Warning";
    public static string Info = @"Info";
    public static string Verbose = @"Verbose";
    public static string Other = @"Easter-Egg";

    private volatile static List<byte[]> queue = new();
    private static Thread workerThread;

    public static void Init()
    {
        workerThread = new Thread(new ThreadStart(WriteQueue));
        workerThread.Start();
    }
    public static void WriteQueue()
    {
        string path = Config.HasInitalized ? Config.Configuration.LogPath : Resource.LogPath;
        Directory.CreateDirectory(path);
        using (FileStream SourceStream = File.Open(path + Resource.LogFileName, FileMode.Append))
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    lock (queue)
                    {
                        byte[] _buffer = queue[^1]; // queue.Count - 1
                        SourceStream.Write(_buffer, 0, _buffer.Length);
                        queue.RemoveAt(queue.Count - 1);
                    }
                }
                Thread.Sleep(50);
            }
        }
    }
    private static bool CheckIgnore(string type)
    {
        if (Config.HasInitalized) { Config.InitializeConfig(); }
        switch (Config.Configuration.VerboseLevel)
        {
            case 0:
                switch (type)
                {
                    case var value when value == Info: // idk why this works
                        return true;
                    case var value when value == Verbose:
                        return true;
                    default:
                        break;
                }
                break;
            case 1:
                switch (type)
                {
                    case var value when value == Verbose:
                        return true;
                    default:
                        break;
                }
                break;
            default:
                return false;
        }
        return false;
    }
    public static void Log(string type, Exception error)
    {
        CheckIgnore(type);
        string errorFormated = string.Format("[{0}] [{1}] {2} at {3};\nInner Exception: {4}\n",
            DateTime.Now,
            type,
            error.Message,
            error.StackTrace,
            (error.InnerException == null) ? "none" : error.InnerException.Message // first time using conditional operator lmao
        );
        queue.Add(Encoding.Unicode.GetBytes(errorFormated));
        Debug.Log(errorFormated);
    }
    public static void Log(string type, string message)
    {
        CheckIgnore(type);
        string errorFormated = string.Format("[{0}] [{1}] {2}\n",
            DateTime.Now,
            type,
            message
        );
        lock (queue) { queue.Add(Encoding.Unicode.GetBytes(errorFormated)); }
        Debug.Log(errorFormated);
    }
}
