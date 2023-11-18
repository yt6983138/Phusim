using System;
using System.IO;
#nullable enable

// b4 u ask why not playerperfs:
// its bc its so fucking hard to change manually by non root users in android
// also i cant use dict in dict with playerperfs

public class Config
{
    public static string ConfigLocation;
    public static ConfigItems Configuration { get; set; }
    public static bool HasInitalized = false;
    // Start is called before the first frame update
    public static void Init()
    {
        try
        {
            ConfigLocation = Resource.ConfigPath + Resource.ConfigFileName;
            LoadConfig();
        }
        catch (Exception ex)
        {
            InitializeConfig();
            throw ex;
        }
    }

    // Update is called once per frame
    public static void InitializeConfig()
    {
        SetDefaultConfig();
        Directory.CreateDirectory(Resource.ConfigPath);
        Directory.CreateDirectory(Configuration.LogPath);
        Directory.CreateDirectory(Configuration.SkinPath);
        Directory.CreateDirectory(Configuration.LanguagePath);
        /*if (!File.Exists(ConfigLocation))
        {
            File.Create(ConfigLocation);
        }*/
        SaveConfig();
        HasInitalized = true;
    }
    public static void LoadConfig()
    {
        //Configuration = JsonSerializer.DeserializeBin<ConfigItems>(File.ReadAllText(ConfigLocation));
        Configuration = Serializer.DeserializeJson<ConfigItems>(ConfigLocation);
        Skins.SkinFileLocation = Configuration.SkinPath + Resource.SkinFileName;
        HasInitalized = true;
    }

    public static void SetDefaultConfig()
    {
        Configuration = Resource.DefaultConfig;
        ConfigLocation = Resource.ConfigPath + Resource.ConfigFileName;
    }
    public static void SaveConfig()
    {
        Serializer.SerializeJson(ConfigLocation, Configuration);
    }
}
#nullable disable