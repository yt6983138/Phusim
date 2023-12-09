using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigItems : ICloneable //fuck this i spent 2hr to figure out bruh
{
    public Dictionary<string, Judgement> Judgements;
    public Dictionary<string, Dictionary<string, object>> Ranks;
    public Vector3 NoteScale;
    public Dictionary<string, float> Volumes;
    public float ChartAspectRatio;
    public string SkinPath;
    public string LanguagePath;
    public string LogPath;
    public int VerboseLevel;
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