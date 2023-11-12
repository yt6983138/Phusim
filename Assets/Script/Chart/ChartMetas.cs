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
    public Dictionary<string, string> ChartName;
    public Dictionary<string, ChartDiffculty> ChartDiffcultys;
    public Dictionary<string, string> MusicsForChart;
    public Dictionary<string, string> BackgroundsForChart;
    public List<string> Charts;
    public Dictionary<string, List<PlayRecord>> PlayRecords;
    public Dictionary<string, (string Composer, string Illustrator, string Charter)> MakerForChart;
}
public class ChartDiffculty
{
    public string Type;
    public float Level;
    public string Format = "{0} Lv.{1}";
    public string FullString
    {
        get
        {
            return string.Format(Format, Type, Level);
        }
    }
}
public class PlayRecord
{
    public DateTime PlayTime;
    public float Acc;
    public float AbsoluteAcc;
    public bool HasFullCombo;
    public float AverageDelay;
}