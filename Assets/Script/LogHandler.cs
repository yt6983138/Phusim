using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class LogHandler : MonoBehaviour
{
    public static string Error = @"Error";
    public static string Warning = @"Warning";
    public static string Info = @"Info";
    public static string Verboose = @"Verboose";
    public static string Other = @"Easter-Egg";

    public static void Log(string type, Exception error)
    {
        switch (Config.ReadConfig().VerbooseLevel)
        {
            case 0:
                switch (type)
                {
                    case var value when value == Info:
                        return;
                    case var value when value == Verboose:
                        return;
                    default:
                        break;
                }
                break;
            case 1:
                switch (type)
                {
                    case var value when value == Verboose:
                        return;
                    default:
                        break;
                }
                break;
            case 2:
                break;
        }
        if (Config.ReadConfig().LogPath == null)
        {
            ConfigItems setPath = Config.ReadConfig();
            setPath.LogPath = StaticUtils.GetDefaultConfigPath() + "latest.log";
            Config.WriteConfig(setPath);
        }
        if (!File.Exists(Config.ReadConfig().LogPath)) { File.Create(Config.ReadConfig().LogPath); }
        File.AppendAllText(Config.ReadConfig().LogPath, string.Format("[{0}] [{1}] {2}\n", DateTime.Now, type, error.Message));
    }
}
