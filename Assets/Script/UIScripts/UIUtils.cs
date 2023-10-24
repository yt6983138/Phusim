using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class UIUtils
{
    public static Vector3 ToGlobalPos(Vector3 pos, RectOffset padding = null)
    {
        if (padding == null) { padding = new() { top = 0, bottom = 0, left = 0, right = 0 }; }
        return new Vector3
        {
            x = (Screen.width - padding.horizontal) / 2 * (pos.x + 1) + padding.left,
            y = (Screen.height - padding.vertical) / 2 * (pos.y + 1) + padding.bottom,
            z = pos.z
        };
    }
    public static Color HexToColor(string hex)
    {
        string splited = hex.Remove(0, 1);
        IEnumerable<string> chunked = Enumerable.Range(0, splited.Length / 2)
        .Select(i => splited.Substring(i * 2, 2));
        return new Color(Convert.ToInt16(chunked.ElementAt(0), 16) / 256, Convert.ToInt16(chunked.ElementAt(1), 16) / 256, Convert.ToInt16(chunked.ElementAt(2), 16) / 256, Convert.ToInt16(chunked.ElementAt(3), 16) / 256);
    }
    public static Texture2D LoadTexture(string path)
    {
        Texture2D texture = new(2, 2);
        if (!texture.LoadImage(File.ReadAllBytes(path)))
        {
            throw new Exception("Cannot load File " + path);
        }
        return texture;
    }
    public static Sprite Image2Sprite(string path, float PixelPerUnit = 100f)
    {
        Texture2D texture = LoadTexture(path);
        return Texture2Sprite(texture, null, PixelPerUnit);
    }
    public static Sprite Texture2Sprite(Texture2D texture, Vector2? pivot = null, float PixelPerUnit = 100f)
    {
        Vector2 _pivot = (pivot == null) ? new Vector2(0, 0) : (Vector2)pivot;
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), _pivot, PixelPerUnit);
    }
    public static Color HSL2RGB(float h, float sl, float l) // https://geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
    {
        float v;
        float r, g, b;

        r = l;   // default to gray
        g = l;
        b = l;
        v = (float)((l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl));

        if (v > 0)
        {
            float m;
            float sv;
            int sextant;
            float fract, vsf, mid1, mid2;


            m = l + l - v;
            sv = (v - m) / v;
            h = h * 6;
            sextant = (int)h;
            fract = h - sextant;
            vsf = v * sv * fract;
            mid1 = m + vsf;
            mid2 = v - vsf;

            switch (sextant)
            {
                case 0:
                    r = v;
                    g = mid1;
                    b = m;
                    break;
                case 1:
                    r = mid2;
                    g = v;
                    b = m;
                    break;
                case 2:
                    r = m;
                    g = v;
                    b = mid1;
                    break;
                case 3:
                    r = m;
                    g = mid2;
                    b = v;
                    break;
                case 4:
                    r = mid1;
                    g = m;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = m;
                    b = mid2;
                    break;
            }
        }
        Color rgb = new()
        {
            r = r,
            g = g,
            b = b
        };
        return rgb;

    }
}