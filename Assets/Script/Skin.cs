using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public static class Skins
{
    public static SkinItems GameSkin { get; set; }
    public static string SkinFileLocation;
    public static Dictionary<string, TMP_FontAsset> FontHolder = new();
    public static Dictionary<string, Texture2D> TextureHolder = new();
    public static void Init()
    {
        try
        {
            throw new Exception();
#pragma warning disable CS0162 // Unreachable code detected
            if (Config.Configuration.SkinPath == string.Empty) {  throw new NullReferenceException("Something is wrong about config file, consider re-generate it"); }
#pragma warning restore CS0162 // Unreachable code detected
            SkinFileLocation = Config.Configuration.SkinPath + Resource.SkinFileName;
            LoadSkin();
        }
        catch (Exception e)
        {
            LoadDefaultSkin();
            SaveSkin();
            throw e;
        }
        finally
        {
            LoadFont("Font.Default", Config.Configuration.SkinPath + "default.ttf");
        }
    }
    public static void LoadSkin()
    {
        GameSkin = Serializer.DeserializeJson<SkinItems>(SkinFileLocation);
    }
    public static void LoadDefaultSkin()
    {
        GameSkin = Resource.DefaultSkin;
        SkinFileLocation = Resource.SkinPath + Resource.SkinFileName;
    }
    public static void SaveSkin() // u shud not use this at anytime (except u wanna impl, in game skin editor?)
    {
        Serializer.SerializeJson(SkinFileLocation, GameSkin);
    }
    public static Texture2D LoadTexture(string key, string state = "Default")
    {
        string nameSpace = string.Format("{0}.{1}", key, state);
        key = key.Replace(".", "/");
        string path = Config.Configuration.SkinPath + Utils.ToPlatformPath(string.Format("{0}/{1}.png", key, state));
        try
        {
            if (TextureHolder.ContainsKey(nameSpace)) { return TextureHolder[nameSpace]; }
            Directory.CreateDirectory(Config.Configuration.SkinPath + key);
            TextureHolder[nameSpace] = UIUtils.LoadTexture(path);
            return TextureHolder[nameSpace];
        }
        catch (Exception e)
        {
            LogHandler.Log(LogHandler.Error, e);
            //Texture2D texture = Resources.Load<Texture2D>(string.Format("{0}.{1}", key, state));
            //texture.Compress(false);

            // i had to use texture.bytes instead texture.png bc its annoying to do a decompress conversation everytime
            Utils.WriteResource(path, Resources.Load<TextAsset>(nameSpace).bytes);
            TextureHolder[nameSpace] = UIUtils.LoadTexture(path);
            return TextureHolder[nameSpace];
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
    public static TMP_FontAsset LoadFont(string key, string path = "")
    {
        try
        {
            if (FontHolder.ContainsKey(key)) { return FontHolder[key]; }
            ReadFont(path, key);
            return FontHolder[key];
        }
        catch (Exception e)
        {
            LogHandler.Log(LogHandler.Error, e);
            TextAsset loadFromAssets = Resources.Load<TextAsset>(key);
            Utils.WriteResource(path, (loadFromAssets == null) ? throw new Exception("Cannot find asset!") : loadFromAssets.bytes);
            ReadFont(path, key);
            return FontHolder[key];
        }
    }
}