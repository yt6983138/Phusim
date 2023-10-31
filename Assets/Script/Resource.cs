using System.Collections.Generic;
using UnityEngine;

public static class Resource
{
    private readonly static string basePath = StaticUtils.GetDefaultConfigPath();
    public readonly static string ConfigPath = basePath;
    public readonly static string ConfigFileName = "config.dat";
    public readonly static string SkinPath = basePath + StaticUtils.ToPlatformPath("Skins/Default/");
    public readonly static string SkinFileName = "skin.dat";
    public readonly static string LangPath = basePath + StaticUtils.ToPlatformPath("Lang/");
    public readonly static string LangFileName = "en.json";
    public readonly static string LogPath = basePath + StaticUtils.ToPlatformPath("Logs/");
    public readonly static string LogFileName = "latest.log";

    public readonly static float DefaultDPI = 150f;

    public readonly static Dictionary<string, string> keyRefrence = new() // to prevent someday ill have to go thru entire program and change smth
    {
        { "Pos", "Pos"},
        { "Scale", "Scale"},
        { "Size", "Size"},
        { "Padding", "Padding"},
        { "Font", "Font"},
        { "FontSize", "FontSize" },
        { "Color", "Color"},
        { "Execute", "Execute" },
        { "BlurRadius", "BlurRadius" },
        { "BlurIteration", "BlurIteration" },
        { "DownSample", "DownSample" },
        { "DownSampleMode", "DownSampleMode" },
        { "RotationEuler", "RotationEuler" }
    };

    public readonly static Dictionary<string, string> LoadingScreenTexts = new() // never meant to be modified
    {
        { "Config", "Configuration System" },
        { "Skin", "Skin System" },
        { "Lang", "Language System" },
        { "Scene.MainUI", "Scenes(MainUI)" },
        { "Loading", @"Loading {0} {1}" }
    };
    public readonly static ConfigItems DefaultConfig = new()
    {
        Judgements = new() {
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
        },
        Ranks = new() {
                { "phi", new Dictionary<string, object> {
                    { "ScoreRequired", 1000000 },
                    { "AccRequired", 1 },
                    { "ComboRequired", -1 },
                    { "ComparingPriority", 0 }}},
                {
        "fc", new Dictionary<string, object> {
                    { "ScoreRequired", 0 },
                    { "AccRequired", 0 },
                    { "ComboRequired", -1 },
                    { "ComparingPriority", 1 }}},
                {
        "vu", new Dictionary<string, object> {
                    { "ScoreRequired", 960000 },
                    { "AccRequired", 0 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 2 }}},
                {
        "s", new Dictionary<string, object> {
                    { "ScoreRequired", 920000 },
                    { "AccRequired", 0 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 3 }}},
                {
        "a", new Dictionary<string, object> {
                    { "ScoreRequired", 880000 },
                    { "AccRequired", 0 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 4 }}},
                {
        "b", new Dictionary<string, object> {
                    { "ScoreRequired", 820000 },
                    { "AccRequired", 0 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 5 }}},
                {
        "c", new Dictionary<string, object> {
                    { "ScoreRequired", 700000 },
                    { "AccRequired", 0 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 6 }}},
                {
        "false", new Dictionary<string, object> {
                    { "ScoreRequired", 0 },
                    { "AccRequired", 0 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 7 }}}
            },
        noteScale = 1,
        Volumes = new() { { "Music", 1 }, { "Effects", 1 }, { "HitSound", 1 } },
        SkinPath = SkinPath,
        LanguagePath = LangPath,
        LogPath = LogPath,
        VerboseLevel = 1
    };
    public readonly static SkinItems DefaultSkin = new()
    {
        SoundEffect = new() { },
        Properties = new() {
            { "Menu.PlayButton", new Dictionary<string, object> {
                { "Pos", new Vector3 { x = 0.4F, y = 0.65F, z = 0F} },
                { "Scale", new Vector3 { x = 1F, y = 1F, z = 1F} },
                { "Size", new PhysicalSize() { xMM = 40, yMM = 20, zMM = 0 } },
                { "RotationEuler", new Vector3 { x = 0f, y = 15f, z = 0f} },
                { "Padding", new RectOffset { top = 0, bottom = 0, left = 0, right = 0 } },
                { "Font", @"Font.Default"},
                { "FontSize", 20f },
                { "Color", new Color(0F, 0F, 0F, 1F) },
                { "Execute", @""}
            } },
            { "Menu.SettingsButton", new Dictionary<string, object> {
                { "Pos", new Vector3 { x = 0.4F, y = 0F, z = 0F} },
                { "Scale", new Vector3 { x = 1F, y = 1F, z = 1F} },
                { "Size", new PhysicalSize() { xMM = 40, yMM = 20, zMM = 0 } },
                { "RotationEuler", new Vector3 { x = 0f, y = 20f, z = 0f} },
                { "Padding", new RectOffset { top = 0, bottom = 0, left = 0, right = 0 } },
                { "Font", @"Font.Default"},
                { "FontSize", 20f },
                { "Color", new Color(0F, 0F, 0F, 1F) },
                { "Execute", new List<object> { "UnityEngine.SceneManagement.SceneManager, UnityEngine.CoreModule", "LoadScene", new object[] { "Settings" } } }
            } },
            { "Menu.SkinButton", new Dictionary<string, object> {
                { "Pos", new Vector3 { x = 0.4F, y = -0.65F, z = 0F} },
                { "Scale", new Vector3 { x = 1F, y = 1F, z = 1F} },
                { "Size", new PhysicalSize () { xMM = 40, yMM = 20, zMM = 0 } },
                { "RotationEuler", new Vector3 { x = 0f, y = 15f, z = 0f} },
                { "Padding", new RectOffset { top = 0, bottom = 0, left = 0, right = 0 } },
                { "Font", @"Font.Default"},
                { "FontSize", 20f },
                { "Color", new Color(0F, 0F, 0F, 1F) },
                { "Execute", @""}
            } },
            { "Menu.Background", new Dictionary<string, object> {
                { "BlurRadius", 10 },
                { "BlurIteration", 1 },
                { "DownSample", 0.25f },
                { "DownSampleMode", FilterMode.Bilinear }
            } }
        }
    };
    public readonly static LanguageItems DefaultLangEn = new()
    {
        Language = new Dictionary<string, string>
        {
            { "Menu.PlayButton", @"Play" },
            { "Menu.SettingsButton", @"Settings" },
            { "Menu.SkinButton", @"Skins" }
        }
    };
}