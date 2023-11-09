using System;
using System.Collections.Generic;
using UnityEngine;

public class Chart
{
    public string Format { get; set; }
    public float Offset { get; set; }
    public List<JudgeLine> JudgeLineList { get; set; }
    public ChartFeatures Features { get; set; }
}
public class ChartFeatures
{
    public bool Use3D { get; set; }
    public bool AllowOverride { get; set; }
    public bool AllowUseExternalTexture { get; set; }
}