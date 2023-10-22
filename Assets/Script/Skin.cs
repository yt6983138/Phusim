using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TMPro;
//using ProtoBuf;

public class Skins
{
    public static SkinItems GameSkin;
    public static string? SkinFileLocation;
    public static Dictionary<string, TMP_FontAsset> FontHolder = new();
    public static void Init()
    {
        try
        {
            SkinFileLocation = Config.ReadConfig().SkinPath + Resource.SkinFileName;
            LoadSkin();
        }
        catch (Exception e)
        {
            LoadDefaultSkin();
            GenerateSkin();
            throw e;
        }
        finally
        {
            LoadFont("Font.Default", Config.ReadConfig().SkinPath + "default.ttf");
        }
    }
    public static void LoadSkin()
    {
        GameSkin = Serializer.DeserializeBin<SkinItems>(SkinFileLocation);
    }
    public static SkinItems ReadSkin()
    {
        return GameSkin;
    }
    public static void LoadDefaultSkin()
    {
        GameSkin = Resource.DefaultSkin;
        SkinFileLocation = Resource.SkinPath + Resource.SkinFileName;
    }
    public static void GenerateSkin() // u shud not use this at anytime (except u wanna impl, in game skin editor?)
    {
        Serializer.SerializeBin(SkinFileLocation, GameSkin);
    }
    public static Texture2D LoadTexture(string key, string state = "Default")
    {
        string path = Config.ReadConfig().SkinPath + StaticUtils.ToPlatformPath(string.Format("{0}/{1}.png", key, state));
        try
        {
            Directory.CreateDirectory(Config.ReadConfig().SkinPath + key);
            return UIUtils.LoadTexture(path);
        }
        catch (Exception e)
        {
            LogHandler.Log(LogHandler.Error, e);
            StaticUtils.WriteBase64Resource(path, Resource.Base64Textures[string.Format("{0}.{1}", key, state)]);
            return UIUtils.LoadTexture(path);
        }
    }
    public static Sprite LoadTextureAsSprite(string key, string state = "Default", Vector2? pivot = null) // for somereason i cant use pivot = Vector2.zero
    {
        pivot = (pivot == null) ? new Vector2 { x = 0, y = 0 } : pivot;
        Texture2D texture = LoadTexture(key, state);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), (Vector2)pivot);
    }
    private static void ReadFont(string path, string name)
    {
        Font font = new Font(path);
        TMP_FontAsset tmpFont = TMP_FontAsset.CreateFontAsset(font);
        FontHolder[name] = tmpFont;
    }
    public static TMP_FontAsset GetFont(string key)
    {
        return FontHolder[key];
    }
    public static TMP_FontAsset LoadFont(string key, string path = null)
    {
        try
        {
            if (FontHolder.ContainsKey(key)) { return FontHolder[key]; }
            if (Resource.Base64Resources.ContainsKey(key))
            { StaticUtils.WriteBase64Resource(path, Resource.Base64Resources[key]); }
            ReadFont(path, key);
            return FontHolder[key];
        }
        catch (Exception e)
        {
            LogHandler.Log(LogHandler.Error, e);
            return null;
        }
    }
}