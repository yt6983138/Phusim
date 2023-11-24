using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ChartManager
{
    private static readonly JsonSerializerSettings _settings = new() { MissingMemberHandling = MissingMemberHandling.Ignore };
    public static System.Diagnostics.Stopwatch Timer { get; private set; } = new();

    public static float ChartAspect { get; private set; } = Config.Configuration.ChartAspectRatio;
    public static AudioClip Bgm { get; private set; } = AudioClip.Create("Empty", 10, 2, 1, false);
    public static AudioSource BgmPlayer { get; private set; }
    public static Texture2D Background { get; private set; } = Texture2D.blackTexture;
    public static Chart CurrentChart { get; private set; }
    public static ChartMeta CurrentChartMeta { get; private set; }
    public static GameObject Canvas { get; private set; }
    public static JudgeLineManager JudgeLineManager { get; private set; }
    public static bool HasEnd { get; private set; } = true;
    public static int EndTimeMs { get; private set; }
    public static float CurrnetProgress { get; } = EndTimeMs / Timer.ElapsedMilliseconds;
    public static Camera Cam { get; private set; }
    public static (float width, float height) ChartWindowSize { get; private set; }

    public static int CurrentPerfect { get; set; } = 0;
    public static int CurrentGood { get; set; } = 0;
    public static int CurrentBad { get; set; } = 0;
    public static int CurrentMiss { get; set; } = 0;

    public static void InitializeEverything(Chart chart, AudioClip bgm, AudioSource player, Texture2D background, ChartMeta meta, ref GameObject objectToDrawOn, ref Camera cam)
    {
        if (!HasEnd)
        {
            throw new Exception("The chart is playing!");
        }
        Bgm = bgm;
        BgmPlayer = player;
        Background = background;
        CurrentChart = chart;
        CurrentChartMeta = meta;
        EndTimeMs = (int)(Bgm.length * 1000);
        Cam = cam;
        Canvas = objectToDrawOn;
        ChartWindowSize = (
            (ChartAspect > Resource.ScreenAspectRatio) ? Screen.width : ChartAspect * (float)Screen.height,
            (ChartAspect < Resource.ScreenAspectRatio) ? Screen.height : (float)Screen.width / ChartAspect
            );
        Timer.Reset();
    }

    public static Chart LoadChart(string path)
    {
        //dynamic chart = Serializer.DeserializeJson<dynamic>(path);
        //UnityEditor.AssetDatabase.LoadAssetAtPath
        switch (GuessChartType(Serializer.DeserializeJson<dynamic>(path)))
        {
            case ChartType.Offical:
                return Serializer.DeserializeJson<OfficalChart>(path, _settings).ToInternalFormat();
            case ChartType.Phusim:
                return Serializer.DeserializeJson<Chart>(path, _settings);
            case ChartType.Rpe:
                throw new NotImplementedException();
            case ChartType.Unknown:
                throw new Exception("Unknown chart type");
            default:
                goto case ChartType.Unknown;
        }
    }
    public static ChartType GuessChartType(dynamic chart)
    {
        try
        {
            if (chart.formatVersion == 3) // it would complain if the chart does not have formatVersion property
            {
                return ChartType.Offical;
            }
            else
            {
                LogHandler.Log(LogHandler.Warning, "loading offical chart with unknown version number: " + chart.formatVersion.ToString());
                return ChartType.Offical;
            }
        }
        catch
        {
            try
            {
                if (chart.META["RPEVersion"] > 0)
                {
                    return ChartType.Rpe;
                }
            }
            catch
            {
                try
                {
                    if (chart.Format.GetType() == typeof(string))
                    {
                        return ChartType.Phusim;
                    }
                }
                catch
                {
                    return ChartType.Unknown;
                }
            }
        }
        return ChartType.Unknown;
    }
    public static List<ChartMeta> GetChartMeta(string ChartFolder)
    {
        List<string> files = new(Directory.GetFiles(ChartFolder));
        foreach (string file in files)
        {
            string filename = Path.GetFileName(file);
            if (filename == "PhusimMeta.json") { return Serializer.DeserializeJson<List<ChartMeta>>(file); }
            try
            {
                return LoadMetaFromDifferentSimulator(file);
            }
            catch { }
        }
        throw new Exception("No meta found");

    }
    public static List<ChartMeta> LoadMetaFromDifferentSimulator(string path)
    {
        string filename = Path.GetFileName(path);
        List<ChartMeta> metas = new();
        // lchzh format start
        if (filename.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
        {
            List<dynamic> otherMeta = Serializer.DeserializeCsv<dynamic>(path);
            foreach (dynamic item in otherMeta)
            {
                ChartMeta meta = new()
                {
                    ChartPath = item.Chart
                };
                StaticUtils.ExecuteWithTry(() => { meta.MusicPath = item.Music; });
                StaticUtils.ExecuteWithTry(() => { meta.BackgroundPath = item.Image; });
                StaticUtils.ExecuteWithTry(() => { meta.ChartName = item.Name; });
                StaticUtils.ExecuteWithTry(() => { meta.Charter = item.Charter; });
                StaticUtils.ExecuteWithTry(() => { meta.Illustrator = item.Illustrator; });
                StaticUtils.ExecuteWithTry(() => { meta.Composer = item.Artist; });
                StaticUtils.ExecuteWithTry(() => { meta.ChartDiffculty = ParseDiffcultyFromString(item.Level); });
                metas.Add(meta);
            }
            return metas;
        } // phira format start
        else if (filename.EndsWith(".yml", StringComparison.CurrentCultureIgnoreCase))
        {
            var otherMeta = Serializer.DeserializeYaml<Dictionary<object, object>>(path);
            ChartMeta meta = new()
            {
                ChartPath = (string)otherMeta["chart"]
            };
            StaticUtils.ExecuteWithTry(() => { meta.MusicPath = (string)otherMeta["music"]; });
            StaticUtils.ExecuteWithTry(() => { meta.BackgroundPath = (string)otherMeta["illustration"]; });
            StaticUtils.ExecuteWithTry(() => { meta.ChartName = (string)otherMeta["name"]; });
            StaticUtils.ExecuteWithTry(() => { meta.Charter = (string)otherMeta["charter"]; });
            StaticUtils.ExecuteWithTry(() => { meta.Illustrator = (string)otherMeta["illustrator"]; });
            StaticUtils.ExecuteWithTry(() => { meta.Composer = (string)otherMeta["composer"]; });

            ChartDiffculty diffculty = new();
            StaticUtils.ExecuteWithTry(() => { diffculty = ParseDiffcultyFromString((string)otherMeta["level"]); });
            diffculty.Level = StaticUtils.ExecuteWithTry<float>(() => { return float.Parse((string)otherMeta["difficulty"]); });
            // for some reason ymlparser think floats are strings bruh
            meta.ChartDiffculty = diffculty;

            metas.Add(meta);
            return metas;
        }
        throw new Exception("Not known/invaild meta format!");
    }
    public static ChartDiffculty ParseDiffcultyFromString(string diffculty)
    {
        string[] splitted = diffculty.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        // note: (char[])null means all types of whitespaces(i got tricked with en-space and normal space bruhhhhhhhh)
        // example format "SP Lv.6969" or "FM  Lv. 1145141919810"
        ChartDiffculty chartDiffculty = new();
        foreach (string line in splitted)
        {
            if (line.StartsWith("Lv.", StringComparison.InvariantCultureIgnoreCase))
            {
                StaticUtils.ExecuteWithTry(() => { chartDiffculty.Level = float.Parse(line.Replace("Lv.", "").Replace(" ", "")); });
            }
            else if (line.Length > 1 && line.Length < 8) { chartDiffculty.Type = line; }
        }

        return chartDiffculty;
    }
}
