using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StaticUtils : MonoBehaviour
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
                return Environment.ExpandEnvironmentVariables(@"%appdata%\Phusim\"); //seens we need pre expand it
            case RuntimePlatform.LinuxPlayer:
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @".config/Phusim/";
            case RuntimePlatform.WindowsEditor:
                return @"..\Phusim\";
            case RuntimePlatform.LinuxEditor:
                return @"../Phusim/";
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
}
