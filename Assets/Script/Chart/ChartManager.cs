using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public static class ChartManager
{
    private static readonly JsonSerializerSettings _settings = new() { MissingMemberHandling = MissingMemberHandling.Ignore };
    private static System.Diagnostics.Stopwatch Timer = new();

    public static float ChartAspect { get; private set; } = Config.Configuration.ChartAspectRatio;
    public static AudioClip Bgm { get; private set; } = AudioClip.Create("Empty", 10, 2, 1, false);
    public static AudioSource BgmPlayer { get; private set; }
    public static Texture2D Background { get; private set; } = Texture2D.blackTexture;

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
        }
        throw new Exception();
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
    public static ChartMeta GetChartMeta(string ChartFolder)
    {
        List<string> files = new(Directory.GetFiles(ChartFolder));
        foreach (string file in files)
        {
            string filename = Path.GetFileName(file);
            if (filename == "PhusimMeta.json") { return Serializer.DeserializeJson<ChartMeta>(file); }
            if (filename.Contains(".csv", StringComparison.CurrentCultureIgnoreCase) || filename.Contains(".yml", StringComparison.CurrentCultureIgnoreCase))
            {
                return LoadMetaFromDifferentSimulator(file);
            }
        }
        return new ChartMeta();

    }
    public static ChartMeta LoadMetaFromDifferentSimulator(string path)
    {
        string filename = Path.GetFileName(path);
        ChartMeta meta = new()
        {
            BackgroundsForChart = new(),
            ChartDiffcultys = new(),
            Charts = new(),
            MakerForChart = new(),
            MusicsForChart = new(),
            PlayRecords = new(),
        };
        // lchzh format start
        if (filename.Contains(".csv", StringComparison.CurrentCultureIgnoreCase))
        {
            List<dynamic> otherMeta = Serializer.DeserializeCsv<dynamic>(path);
            foreach (dynamic item in otherMeta)
            {
                meta.Charts.Add(item.Chart);
                meta.MusicsForChart.Add(item.Chart, item.Music);
                meta.BackgroundsForChart.Add(item.Chart, item.Image);
                meta.ChartName.Add(item.Chart, item.Name);
                meta.MakerForChart.Add(item.Chart, (item.Artist, item.Illustrator, item.Charter));
                meta.ChartDiffcultys.Add(item.Chart, ParseDiffcultyFromString(item.Level));
            }
            return meta;
        } // phira start
        else if (filename.Contains(".yml", StringComparison.CurrentCultureIgnoreCase))
        {
            Dictionary<string, object> items = Serializer.DeserializeYamlSimple(path);
            string chartFile = (string)items["chart"];

            meta.Charts.Add(chartFile);
            meta.MusicsForChart.Add(chartFile, (string)items["music"]);
            meta.BackgroundsForChart.Add(chartFile, (string)items["illustration"]);
            meta.ChartName.Add(chartFile, (string)items["name"]);
            meta.MakerForChart.Add(chartFile, ((string)items["composer"], (string)items["illustrator"], (string)items["charter"]));
            ChartDiffculty level = ParseDiffcultyFromString((string)items["level"]);
            level.Level = (float)items["difficulty"];
            meta.ChartDiffcultys.Add(chartFile, level);

            return meta;
        }
        return null;
    }
    public static ChartDiffculty ParseDiffcultyFromString(string diffculty)
    {
        string[] splitted = diffculty.Split(' ');
        string type = "Unknown";
        string level = "-32768";
        float realLevel = -32768;
        foreach (string line in splitted)
        {
            if (line.StartsWith("Lv.", StringComparison.InvariantCultureIgnoreCase))
            {
                level = line.Replace("Lv.", "").Replace(" ", "");
                try { realLevel = float.Parse(line); } catch { }
            }
            else if (line.Length > 1 && line.Length < 8) { type = line; }
        }

        return new ChartDiffculty()
        {
            Type = type,
            Level = realLevel
        };
    }
}