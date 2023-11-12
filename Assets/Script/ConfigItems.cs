using OdinSerializer;
using System;
using System.Collections.Generic;
#nullable enable

//[Serializable]
public class ConfigItems : ICloneable //fuck this i spent 2hr to figure out bruh
{
    public Dictionary<string, Dictionary<string, object>>? Judgements { get; set; }
    public Dictionary<string, Dictionary<string, object>>? Ranks { get; set; }
    public float? NoteScale { get; set; }
    public Dictionary<string, float>? Volumes { get; set; }
    public float ChartAspectRatio { get; set; }
    public string? SkinPath { get; set; }
    public string? LanguagePath { get; set; }
    public string? LogPath { get; set; }
    public int? VerboseLevel { get; set; }
    public object Clone()
    {
        return new ConfigItems
        {
            Judgements = this.Judgements,
            Ranks = this.Ranks,
            NoteScale = this.NoteScale,
            Volumes = this.Volumes,
            SkinPath = this.SkinPath,
            LanguagePath = this.LanguagePath,
            LogPath = this.LogPath,
            VerboseLevel = this.VerboseLevel
        };
    }
}
#nullable disable
