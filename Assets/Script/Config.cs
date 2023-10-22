using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using System;
using System.IO;
#nullable enable

// b4 u ask why not playerperfs:
// its bc its so fucking hard to change manually by non root users in android
// also i cant use dict in dict with playerperfs

public class Config
{
    public static string? ConfigLocation;
    private static ConfigItems? Configuration = null;
    // Start is called before the first frame update
    public static void Init()
    {        
        try
        {
            ConfigLocation = Resource.ConfigPath + Resource.ConfigFileName;
            LoadConfig();
        } catch (Exception ex)
        {
            InitializeConfig();
            throw ex;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public static void InitializeConfig()
    {
        SetDefaultConfig();
        Directory.CreateDirectory(Resource.ConfigPath);
        Directory.CreateDirectory(Configuration.LogPath);
        Directory.CreateDirectory(Configuration.SkinPath);
        Directory.CreateDirectory(Configuration.LanguagePath);
        if (!File.Exists(ConfigLocation))
        {
            File.Create(ConfigLocation);
        }
        SaveConfig();
    }
    public static void LoadConfig()
    {
        //Configuration = JsonSerializer.DeserializeBin<ConfigItems>(File.ReadAllText(ConfigLocation));
        Configuration = Serializer.DeserializeBin<ConfigItems>(ConfigLocation);
        Skins.SkinFileLocation = Configuration.SkinPath + Resource.SkinFileName;
     }

    public static void SetDefaultConfig()
    {
        Configuration = Resource.DefaultConfig;
        ConfigLocation = Resource.ConfigPath + Resource.ConfigFileName;
    }

    public static ConfigItems ReadConfig()
    {
        return Configuration;
    }

    public static void WriteConfig(ConfigItems config)
    {
        Configuration = config;
    }
    public static void SaveConfig()
    {
        Serializer.SerializeBin(ConfigLocation, Configuration);
    }
}
#nullable disable