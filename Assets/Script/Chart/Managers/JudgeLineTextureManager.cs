using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class JudgeLineTextureManager
{
    private static readonly Sprite Unset = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height), new Vector2(0, 0));

    public static Sprite LineAP { get; private set; } = Unset;
    public static Sprite LineFC { get; private set; } = Unset;
    public static Sprite LineMissed { get; private set; } = Unset;
    public static GameObject LineTemplate { get; private set; }
    public static bool HasInitialized { get; private set; } = false;

    public static Dictionary<int, Sprite> TextureOverride { get; set; } = new();

    public static void Initialize(Sprite ap, Sprite fc, Sprite miss, GameObject template)
    {
        if (HasInitialized) { throw new Exception("Already Initialized"); }
        TextureOverride.Clear();
        LineAP = ap;
        LineFC = fc;
        LineMissed = miss;
        LineTemplate = template;
        HasInitialized = true;
    }
    public static void Reset()
    {
        TextureOverride.Clear();
        LineAP = Unset;
        LineFC = Unset;
        LineMissed = Unset;
        HasInitialized = false;
    }
    public static Sprite GetSpriteForLine(in JudgeLineInternalFormat line)
    {
        if (line.UseOverrideTexture)
        {
            return TextureOverride.ContainsKey(line.Id) ? TextureOverride[line.Id] : throw new Exception(string.Format("JudgeLineInternalFormat id {0} do not have texture override registered!", line.Id.ToString()));
        }
        if (ChartManager.CurrentMiss > 0 || ChartManager.CurrentBad > 0) { return LineMissed; }
        else if (ChartManager.CurrentGood > 0) { return LineFC; }
        else { return LineAP; }
    }
}
