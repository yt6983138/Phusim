using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using ProtoBuf;
#nullable enable

//[ProtoContract]
[Serializable]
public class ConfigItems : ICloneable //fuck this i spent 2hr to figure out bruh
{
    /* public Dictionary<string, dynamic> Items = new() { 
        { "Judgements", new Dictionary<string, int> { } },
        { "noteScale",  new float { } },
        { "Volumes", new Dictionary<string, float> { } },
        { "Paths", new Dictionary<string, string> { } }
    };*/
    //[ProtoMember(1)]
    public Dictionary<string, Dictionary<string, object>>? Judgements { get; set; }
    //[ProtoMember(5)]
    public Dictionary<string, Dictionary<string, object>>? Ranks { get; set; }
    //[ProtoMember(10)]
    public float? noteScale { get; set; }
    //[ProtoMember(15)]
    public Dictionary<string, float>? Volumes { get; set; }
    //[ProtoMember(20)]
    public string? SkinPath { get; set; }
    //[ProtoMember(25)]
    public string? LanguagePath { get; set; }
    //[ProtoMember(30)]
    public string? LogPath { get; set; }
    //[ProtoMember(35)]
    public int? VerboseLevel { get; set; }
    public object Clone()
    {
        return new ConfigItems
        {
            Judgements = this.Judgements,
            Ranks = this.Ranks,
            noteScale = this.noteScale,
            Volumes = this.Volumes,
            SkinPath = this.SkinPath,
            LanguagePath = this.LanguagePath,
            LogPath = this.LogPath,
            VerboseLevel = this.VerboseLevel
        };
    }
}
#nullable disable
