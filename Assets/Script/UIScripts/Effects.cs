using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Effects
{
    public static Texture2D ApplyEffects(Texture2D texture, List<Dictionary<string,object>> effectsToApply)
    {
        foreach (var item in effectsToApply)
        {
            switch (item["Name"])
            {
                case "Blur":
                    if (item["DownSample"] is float)
                    {
                        return LinearBlur.Blur(texture, (int)item["BlurRadius"], (int)item["BlurIteration"], (float)item["DownSample"], (FilterMode)item["DownSampleMode"]);
                    } else
                    {
                        return LinearBlur.Blur(texture, (int)item["BlurRadius"], (int)item["BlurIteration"], (Vector2)item["DownSample"], (FilterMode)item["DownSampleMode"]);
                    }
                default:
                    throw new Exception("Unknown/Not Implemented effect: " + item["Name"]);
            }
        }
        return texture;
    }
}
