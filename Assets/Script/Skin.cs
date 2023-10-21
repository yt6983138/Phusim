using UnityEngine;
using System;
using System.IO;
//using ProtoBuf;

public class Skins
{
    public static SkinItems GameSkin;
    public static string? SkinFileLocation;
    public static void Init()
    {
        try
        {
            Debug.Log(Config.ReadConfig().SkinPath);
            Debug.Log(Config.ReadConfig());
            SkinFileLocation = Config.ReadConfig().SkinPath + Resource.SkinFileName;
            LoadSkin();
        } catch (Exception e)
        {
            LoadDefaultSkin();
            GenerateSkin();
            throw e;
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
}