using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
#nullable enable

public class ConfigItems : MonoBehaviour
{
    /* public Dictionary<string, dynamic> Items = new() { 
        { "Judgements", new Dictionary<string, int> { } },
        { "noteScale",  new float { } },
        { "Volumes", new Dictionary<string, float> { } },
        { "Paths", new Dictionary<string, string> { } }
    };*/
    public Dictionary<string, Dictionary<string, object>>? Judgements { get; set; }
    public Dictionary<string, Dictionary<string, object>>? Ranks { get; set; }
    public float? noteScale { get; set; }
    public Dictionary<string, float>? Volumes { get; set; }
    public string? SkinPath { get; set; }
    public string? LanguagePath { get; set; }
    public string? LogPath { get; set; }
    public int? VerbooseLevel { get; set; }
}
#nullable disable
