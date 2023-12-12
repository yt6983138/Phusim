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
        //this.JudgeLineList.Sort();
        for (int i = 0; i < JudgeLineList.Count; i++)
        {
            this.JudgeLineList[i].Initalize(canva);
            this.JudgeLineList[i].Id = i;
        }
    }
    public void Update(int currentMS)
    {
        foreach (JudgeLineInternalFormat line in this.JudgeLineList)
        {
            line.Update(currentMS);
        }
    }
}
public class ChartFeatures
{
    public bool Use3D { get; set; }
    public bool AllowOverride { get; set; }
    public bool AllowUseExternalTexture { get; set; }
}