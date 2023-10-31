using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class StaticUtils
{
    // Start is called before the first frame update
    public static string GetDefaultConfigPath()
    {
        UnityEngine.RuntimePlatform platform = Application.platform;
        switch (platform)
        {
            case RuntimePlatform.Android:
                return @"/sdcard/Android/data/com.me.Phusim/";
            case RuntimePlatform.WindowsPlayer:
                return Environment.ExpandEnvironmentVariables(@"%appdata%\Phusim\"); //seems we need pre expand it
            case RuntimePlatform.LinuxPlayer:
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @".config/Phusim/";
            case RuntimePlatform.WindowsEditor:
                return @"..\PhusimConfig\";
            case RuntimePlatform.LinuxEditor:
                return @"../PhusimConfig/";
            default:
                throw new Exception("Unsupported Platform: " + Application.platform.ToString());
                // no apple shits
        }
    }
    public static string ToPlatformPath(string path)
    {
        UnityEngine.RuntimePlatform platform = Application.platform;
        switch (platform)
        {
            case RuntimePlatform.WindowsEditor:
                return path.Replace(@"/", @"\");
            case RuntimePlatform.WindowsPlayer:
                return path.Replace(@"/", @"\");
            default:
                return path.Replace(@"\", @"/");
        }
    }
    public static Vector3 LengthToScale(List<float> size, GameObject obj)
    {
        Vector3 scale;
        scale.x = size[0] * obj.transform.localScale.x;
        scale.y = size[1] * obj.transform.localScale.y;
        scale.z = size[2] * obj.transform.localScale.z;
        return scale;
    }
    public static (string space, string method, object[] args) ExecuteToNamedTuple(List<object> tuple)
    {
        (string space, string method, object[] args) t = ((string)tuple.ElementAt(0), (string)tuple.ElementAt(1), (object[])tuple.ElementAt(2));
        return t;
    }
    public static Type[] ObjectToTypeArray(object[] obj)
    {
        Type[] typeArray = new Type[obj.Length];
        int i = 0;
        foreach (object o in obj)
        {
            typeArray[i] = o.GetType();
            i++;
        }
        return typeArray;
    }
    public static void WriteResource(string path, byte[] resource, FileMode fileMode = FileMode.OpenOrCreate)
    {
        using (FileStream file = File.Open(path, fileMode))
        {
            file.Write(resource);
        }
    }
}
