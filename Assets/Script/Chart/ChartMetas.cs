using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ChartType
{
    Offical = 0,
    Phusim = 1,
    Rpe = 2,
    Unknown = 3
}
public class ChartResources
{
    public string ChartFolder;
    public ChartType ChartType;
    public List<string> ChartFilenames;
    public List<string> BackgroundFilenames;
    public List<string> BgmFilenames;
    public string MetaFilename;
}
public class ChartMeta
{
    public string ChartName = "Unset/Unknown";
    public ChartDiffculty ChartDiffculty = new();
    public string MusicPath;
    public string BackgroundPath;
    public string ChartPath;
    public List<PlayRecord> PlayRecords = new();
    public string Composer = "Unset/Unknown";
    public string Illustrator = "Unset/Unknown";
    public string Charter = "Unset/Unknown";
    public override string ToString()
    {
        return $"{{ Name = {ChartName}, \n" +
            $"Diffculty = {{ {ChartDiffculty} }}, \n" +
            $"MusicPath = {MusicPath}, \n" +
            $"BackgroundPath = {BackgroundPath}, \n" +
            $"ChartPath = {ChartPath}, \n" +
            $"PlayRecords = {{ {string.Join(", ", PlayRecords)} }}, \n" +
            $"Composer = {Composer}, \n" +
            $"Illustrator = {Illustrator}, \n" +
            $"Charter = {Charter} }}";
    }
}
public class ChartDiffculty
{
    public string Type = "Unknown";
    public float Level = -32768;
    public string Format = "{0} Lv.{1}";
    public string FullString
    {
        get
        {
            return string.Format(Format, Type, Level);
        }
    }
    public override string ToString()
    {
        return $"Format = {Format}, Type = {Type}, Level = {Level}";
    }
}
public class PlayRecord
{
    public DateTime PlayTime = new(2069, 69, 69);
    public float Acc = 0f;
    public float AbsoluteAcc = 0f;
    public bool FullComboed = false;
    public float AverageDelay = 0f;
}