using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using System;
using System.IO;
#nullable enable

public class Config : MonoBehaviour
{
    public static string? ConfigLocation;
    private static ConfigItems? Configuration { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            ConfigLocation = StaticUtils.GetDefaultConfigPath() + "config.json";
            LoadConfig();
        } catch (Exception e)
        {
            InitializeConfig();
            LogHandler.Log(LogHandler.Error, e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void InitializeConfig()
    {
        SetDefaultConfig();
        Directory.CreateDirectory(ConfigLocation);
        Directory.CreateDirectory(Configuration.LogPath);
        Directory.CreateDirectory(Configuration.SkinPath);
        Directory.CreateDirectory(Configuration.LanguagePath);
        SaveConfig();
    }
    public static void LoadConfig()
    {
        Configuration = JsonSerializer.Deserialize<ConfigItems>(File.ReadAllText(ConfigLocation));
        Skins.SkinFileLocation = Configuration.SkinPath + @"skins.json";
     }

    public static void SetDefaultConfig()
    {
        ConfigItems DefaultConfig = new ConfigItems();
        string basePath = StaticUtils.GetDefaultConfigPath();
        /*DefaultConfig.Judgements = new() { { "perfectEarly", new Dictionary<string, object> { 80, 40, 1, 1, 0.9, 1 } }, 
                                           { "perfectLate", new Dictionary<string, object> { 80, 40, 1, 1, 0.9, 1 } },
                                           { "goodEarly",  new Dictionary<string, object> { 160, 75, 0.65, 0.65, 0.585, 0.65 } }, 
                                           { "goodLate", new Dictionary<string, object> { 160, 75, 0.65, 0.65, 0.585, 0.65 } },
                                           { "badEarly",  new Dictionary<string, object> { 180, 140, 0, 0, 0, 0 } }, 
                                           { "badLate", new Dictionary<string, object> { 180, 140, 0, 0, 0, 0 } },
                                           { "miss", new Dictionary<string, object> { -1, -1, 0, 0, 0, 0} }, 
                                           { "combo", new Dictionary<string, object> { -1, -1, 0, 0, 0.1, 0} }
                                            };*/
        // old format ^^^^
        DefaultConfig.Judgements = new() { 
            { "perfectEarly", new Dictionary<string, object> { 
                { "JudgementNormalMs", 80 },
                { "JudgementChallengeMs", 40},
                { "accGivenNormal", 1},
                { "accGivenChallenge", 1 },
                { "ScoreGivenNormal", 0.9},
                { "ScoreGivenChallenge", 1}
            } },
            { "perfectLate", new Dictionary<string, object> {
                { "JudgementNormalMs", 80 },
                { "JudgementChallengeMs", 40},
                { "accGivenNormal", 1},
                { "accGivenChallenge", 1 },
                { "ScoreGivenNormal", 0.9},
                { "ScoreGivenChallenge", 1}
            } },
            { "goodEarly",  new Dictionary<string, object> {
                { "JudgementNormalMs", 160 },
                { "JudgementChallengeMs", 75},
                { "accGivenNormal", 0.65},
                { "accGivenChallenge", 0.65 },
                { "ScoreGivenNormal", 0.585},
                { "ScoreGivenChallenge", 0.65}
            } },
            { "goodLate", new Dictionary<string, object> {
                { "JudgementNormalMs", 160 },
                { "JudgementChallengeMs", 75},
                { "accGivenNormal", 0.65},
                { "accGivenChallenge", 0.65 },
                { "ScoreGivenNormal", 0.585},
                { "ScoreGivenChallenge", 0.65}
            } },
            { "badEarly",  new Dictionary<string, object> {
                { "JudgementNormalMs", 180 },
                { "JudgementChallengeMs", 140},
                { "accGivenNormal", 0},
                { "accGivenChallenge", 0 },
                { "ScoreGivenNormal", 0},
                { "ScoreGivenChallenge", 0}
            } },
            { "badLate", new Dictionary<string, object> {
                { "JudgementNormalMs", 180 },
                { "JudgementChallengeMs", 140},
                { "accGivenNormal", 0},
                { "accGivenChallenge", 0 },
                { "ScoreGivenNormal", 0},
                { "ScoreGivenChallenge", 0}
            } },
            { "miss", new Dictionary<string, object> {
                { "JudgementNormalMs", -1 },
                { "JudgementChallengeMs", -1},
                { "accGivenNormal", 0},
                { "accGivenChallenge", 0 },
                { "ScoreGivenNormal", 0},
                { "ScoreGivenChallenge", 0}
            } },
            { "combo", new Dictionary<string, object> {
                { "JudgementNormalMs", -1 },
                { "JudgementChallengeMs", -1},
                { "accGivenNormal", 0},
                { "accGivenChallenge", 0 },
                { "ScoreGivenNormal", 0},
                { "ScoreGivenChallenge", 0}
            } }
        };
        // btw fuck c# tuples
        DefaultConfig.Ranks = new() {
            { "phi", new Dictionary<string, object> { 
                { "ScoreRequired", 1000000 },
                { "AccRequired", 1 },
                { "ComboRequired", -1 },
                { "ComparingPriority", 0 }}},
            { "fc", new Dictionary<string, object> {
                { "ScoreRequired", 0 },
                { "AccRequired", 0 },
                { "ComboRequired", -1 },
                { "ComparingPriority", 1 }}},
            { "vu", new Dictionary<string, object> {
                { "ScoreRequired", 960000 },
                { "AccRequired", 0 },
                { "ComboRequired", 0 },
                { "ComparingPriority", 2 }}},
            { "s", new Dictionary<string, object> {
                { "ScoreRequired", 920000 },
                { "AccRequired", 0 },
                { "ComboRequired", 0 },
                { "ComparingPriority", 3 }}},
            { "a", new Dictionary<string, object> {
                { "ScoreRequired", 880000 },
                { "AccRequired", 0 },
                { "ComboRequired", 0 },
                { "ComparingPriority", 4 }}},
            { "b", new Dictionary<string, object> {
                { "ScoreRequired", 820000 },
                { "AccRequired", 0 },
                { "ComboRequired", 0 },
                { "ComparingPriority", 5 }}},
            { "c", new Dictionary<string, object> {
                { "ScoreRequired", 700000 },
                { "AccRequired", 0 },
                { "ComboRequired", 0 },
                { "ComparingPriority", 6 }}},
            { "false", new Dictionary<string, object> {
                { "ScoreRequired", 0 },
                { "AccRequired", 0 },
                { "ComboRequired", 0 },
                { "ComparingPriority", 7 }}}
        };
        DefaultConfig.noteScale = 1;
        DefaultConfig.Volumes = new() { { "Music", 1 }, { "Effects", 1 }, { "HitSound", 1 } };
        DefaultConfig.SkinPath = StaticUtils.ToPlatformPath(basePath + "Skins/");
        DefaultConfig.LanguagePath = StaticUtils.ToPlatformPath(basePath + "Langs/");
        DefaultConfig.LogPath = StaticUtils.ToPlatformPath(basePath + "Logs/");
        DefaultConfig.VerbooseLevel = 1;
        Configuration = DefaultConfig;
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
        string SerializedConfig = JsonSerializer.Serialize(Configuration);
        File.WriteAllText(ConfigLocation, SerializedConfig);
    }
}
#nullable disable