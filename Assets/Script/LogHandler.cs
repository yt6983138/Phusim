using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class LogHandler
{
    public static string Error = @"Error";
    public static string Warning = @"Warning";
    public static string Info = @"Info";
    public static string Verbose = @"Verbose";
    public static string Other = @"Easter-Egg";
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
        string path;
        try 
        { 
            path = Config.ReadConfig().LogPath;
            Directory.CreateDirectory(path);
        } catch {
            path = Resource.LogPath;
            Directory.CreateDirectory(path);
        }
        using (FileStream SourceStream = File.Open(path + Resource.LogFileName, FileMode.Append))
        {
            while (true)
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(buffer, 0, buffer.Length);
                buffer = new byte[0];
            }
        }
    }
    public static void Log(string type, Exception error)
    {
        try
        {
            Config.LoadConfig();
            switch (Config.ReadConfig().VerboseLevel)
            {
                case 0:
                    switch (type)
                    {
                        case var value when value == Info:
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
        }
        catch
        {
            Config.InitializeConfig();
        }
        buffer = Encoding.Unicode.GetBytes(string.Format("[{0}] [{1}] {2}\nInner Exception: {3}\n", DateTime.Now, type, error.Message, error.InnerException.Message));
        Debug.Log(System.Text.Encoding.Unicode.GetString(buffer));
    }
}
