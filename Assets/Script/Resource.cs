using System.Collections.Generic;
using UnityEngine;

public static class Resource
{
    private readonly static string basePath = StaticUtils.GetDefaultConfigPath();
    public readonly static string ConfigPath = basePath;
    public readonly static string ConfigFileName = "Config.json";
    public readonly static string SkinPath = basePath + StaticUtils.ToPlatformPath("Skins/Default/");
    public readonly static string SkinFileName = "Skin.json";
    public readonly static string LangPath = basePath + StaticUtils.ToPlatformPath("Lang/");
    public readonly static string LangFileName = "en_US.json";
    public readonly static string LogPath = basePath + StaticUtils.ToPlatformPath("Logs/");
    public readonly static string LogFileName = "Latest.log";

    public readonly static float ScreenAspectRatio = (float)Screen.width / (float)Screen.height;

    public const float DefaultDPI = 150f;
    public const float Ratio16_9 = 16f / 9f;
    public const float OfficalChartPosXMagicNumber = 8.95522403717041f;

    public readonly static Dictionary<string, string> keyRefrence = new() // to prevent someday ill have to go thru entire program and change smth
    {
        { "Pos", "Pos"},
        { "Scale", "Scale"},
        { "Size", "Size"},
        { "Padding", "Padding"},
        { "Font", "Font"},
        { "FontSize", "FontSize" },
        { "Color", "Color"},
        { "Effects", "Effects" },
        { "RotationEuler", "RotationEuler" }
    };

    public readonly static Dictionary<string, (string info, string method, object[] args)> InvokeInfo = new()
    {
        { "Menu.PlayButton", ("UnityEngine.SceneManagement.SceneManager, UnityEngine.CoreModule", "LoadScene", new object[] { "PlayChart" }) }
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
            { "Perfect", new Judgement()
            {
                JudgementRange = (-80, 80),
                AccGiven = 1f,
                ComparingPriority = 0
            } },
            { "Good", new Judgement()
            {
                JudgementRange = (-160, 160),
                AccGiven = 0.65f,
                ComparingPriority = 1
            } },
            { "Bad", new Judgement()
            {
                JudgementRange = (-180, 180),
                AccGiven = 0f,
                ComparingPriority = 2
            } },
            { ".DragAndFlick", new Judgement() {
                JudgementRange = (0, 180),
                AccGiven = 1f,
            } },
            { ".HoldMaxUnholdTime", new Judgement() {
                JudgementRange = (0, 40)
            } }
        },
        Ranks = new() { {
                "Phi", new Dictionary<string, object> {
                    { "AccRequired", 1 },
                    { "ComboRequired", -1 },
                    { "ComparingPriority", 0 } } },
            {
                "Vu", new Dictionary<string, object> {
                    { "AccRequired", 0.975 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 1 } } },
            {
                "Success", new Dictionary<string, object> {
                    { "AccRequired", 0.9 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 2 } } },
            {
                "Failed", new Dictionary<string, object> {
                    { "AccRequired", 0 },
                    { "ComboRequired", 0 },
                    { "ComparingPriority", 3 } } }
        },
        NoteScale = 1,
        Volumes = new() { { "Music", 1 }, { "Effects", 1 }, { "HitSound", 1 } },
        ChartAspectRatio = ((ScreenAspectRatio) < Ratio16_9) ? (ScreenAspectRatio) : Ratio16_9,
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
                { "Pos", new CustomSize { PosX = 0.4F, PosY = 0.65F, PosZ = 0F} },
                { "Scale", new CustomSize { PosX = 1F, PosY = 1F, PosZ = 1F} },
                { "Size", new CustomSize() { xMM = 40, yMM = 20, zMM = 0 } },
                { "RotationEuler", new CustomSize {PosX = 0f, PosY = 15f, PosZ = 0f} },
                { "Padding", new RectOffset { top = 0, bottom = 0, left = 0, right = 0 } },
                { "Font", @"Font.Default"},
                { "FontSize", 20f },
                { "Color", new Color(0F, 0F, 0F, 1F) }
            } },
            { "Menu.SettingsButton", new Dictionary<string, object> {
                { "Pos", new CustomSize {PosX = 0.4F, PosY = 0F, PosZ = 0F} },
                { "Scale", new CustomSize { PosX = 1F, PosY = 1F, PosZ = 1F} },
                { "Size", new CustomSize() { xMM = 40, yMM = 20, zMM = 0 } },
                { "RotationEuler", new CustomSize { PosX = 0f, PosY = 15f, PosZ = 0f} },
                { "Padding", new RectOffset { top = 0, bottom = 0, left = 0, right = 0 } },
                { "Font", @"Font.Default"},
                { "FontSize", 20f },
                { "Color", new Color(0F, 0F, 0F, 1F) }
            } },
            { "Menu.SkinButton", new Dictionary<string, object> {
                { "Pos", new CustomSize { PosX = 0.4F, PosY = -0.65F, PosZ = 0F} },
                { "Scale", new CustomSize {PosX = 1F, PosY = 1F, PosZ = 1F} },
                { "Size", new CustomSize () { xMM = 40, yMM = 20, zMM = 0 } },
                { "RotationEuler", new CustomSize {PosX = 0f, PosY = 15f, PosZ = 0f} },
                { "Padding", new RectOffset { top = 0, bottom = 0, left = 0, right = 0 } },
                { "Font", @"Font.Default"},
                { "FontSize", 20f },
                { "Color", new Color(0F, 0F, 0F, 1F) }
            } },
            { "MainUI.Background", new Dictionary<string, object> {
                { "Effects", new List<Dictionary<string,object>> {
                    new Dictionary<string, object> {
                        { "Name", "Blur" },
                        { "BlurRadius", 10 },
                        { "BlurIteration", 1 },
                        { "DownSample", 0.25f },
                        { "DownSampleMode", FilterMode.Bilinear }
                    }
                } }
            } },
            { "PlayChart.Overlay.ProgressBar", new Dictionary<string, object> {
                { "Height", new CustomSize() { yMM = 3} },
                { "ProgressedFillColor", new Color(0.5f, 0.5f, 0.5f, 1) },
                { "UnprogressedFillColor", new Color(0, 0, 0, 0) },
                { "HandleColor", new Color(1, 1, 1, 1) }
            } },
            { "PlayChart.Overlay.LeftPanel", new Dictionary<string, object> {
                { "Color", new Color(0.3f, 0.3f, 0.3f, 0.6f) }
            } },
            { "PlayChart.Overlay.RightPanel", new Dictionary<string, object> {
                { "Color", new Color(0.3f, 0.3f, 0.3f, 0.6f) }
            } },
            { "PlayChart.Background", new Dictionary<string, object> {
                { "Effects", new List<Dictionary<string,object>> {
                    new Dictionary<string, object> {
                        { "Name", "Blur" },
                        { "BlurRadius", 10 },
                        { "BlurIteration", 1 },
                        { "DownSample", 0.25f },
                        { "DownSampleMode", FilterMode.Bilinear }
                    }
                } }
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