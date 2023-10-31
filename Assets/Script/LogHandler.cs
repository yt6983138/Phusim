using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LogHandler
{
    public static string Error = @"Error";
    public static string Warning = @"Warning";
    public static string Info = @"Info";
    public static string Verbose = @"Verbose";
    public static string Other = @"Easter-Egg";

    private static byte[] zero = new byte[0];
    private static byte[] buffer = { };

    public static void Init()
    {
        Task.Run(() =>
        {
            WriteBuffer();
        });
    }
    public static async Task WriteBuffer()
    {
        string path = Config.HasInitalized ? Config.ReadConfig().LogPath : Resource.LogPath;
        Directory.CreateDirectory(path);
        using (FileStream SourceStream = File.Open(path + Resource.LogFileName, FileMode.Append))
        {
            while (true)
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(buffer, 0, buffer.Length);
                buffer = zero;
            }
        }
    }
    public static void Log(string type, Exception error)
    {
        if (Config.HasInitalized) { Config.InitializeConfig(); }
        switch (Config.ReadConfig().VerboseLevel)
        {
            case 0:
                switch (type)
                {
                    case var value when value == Info: // idk why this works
                        return;
                    case var value when value == Verbose:
                        return;
                    default:
                        break;
                }
                break;
            case 1:
                switch (type)
                {
                    case var value when value == Verbose:
                        return;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        string errorFormated = string.Format("[{0}] [{1}] {2} at {3};\nInner Exception: {4}\n",
            DateTime.Now,
            type,
            error.Message,
            error.StackTrace,
            (error.InnerException == null) ? "none" : error.InnerException.Message // first time using conditional operator lmao
        );
        buffer = Encoding.Unicode.GetBytes(errorFormated);
        Debug.Log(errorFormated);
    }
}
