using System;
using System.Collections.Generic;
using UnityEngine;

public class ChartInternalFormat
{
    public string Format { get; set; }
    public float Offset { get; set; }
    public List<JudgeLineInternalFormat> JudgeLineList { get; set; }
    public ChartFeatures Features { get; set; }
    public void Process(in GameObject canva)
    {
        for (int i = 0; i < JudgeLineList.Count; i++)
        {
            JudgeLineList[i].Initalize(canva);
            JudgeLineList[i].Id = i;
        }
    }
}
public class ChartFeatures
{
    public bool Use3D { get; set; }
    public bool AllowOverride { get; set; }
    public bool AllowUseExternalTexture { get; set; }
}