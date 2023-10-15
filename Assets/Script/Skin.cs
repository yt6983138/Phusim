using UnityEngine;
using System.Collections;
using System;
using System.Text.Json;
using System.IO;

public class Skins : MonoBehaviour
{
    private static SkinItems? GameSkin;
    public static string? SkinFileLocation;
    void Start()
    {
        
    }
    public static void LoadSkin()
    {
        GameSkin = JsonSerializer.Deserialize<SkinItems>(File.ReadAllText(SkinFileLocation));
    }
    public static SkinItems ReadSkin()
    {
        return GameSkin;
    }
    public static void LoadDefaultSkin()
    {
        SkinItems defaultSkin = new();

    }
}