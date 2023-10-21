using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using SixLabors;

public class UIUtils
{
    public static Vector3 ToGlobalPos(Vector3 pos)
    {
        return new Vector3 { x = Screen.width / 2 * (pos.x + 1), y = Screen.height / 2 * (pos.y + 1), z = pos.z };
    }
    public static Color HexToColor(string hex)
    {
        string splited = hex.Remove(0, 1);
        IEnumerable<string> chunked = Enumerable.Range(0, splited.Length / 2)
        .Select(i => splited.Substring(i * 2, 2));
        return new Color(Convert.ToInt16(chunked.ElementAt(0), 16) / 256, Convert.ToInt16(chunked.ElementAt(1), 16) / 256, Convert.ToInt16(chunked.ElementAt(2), 16) / 256, Convert.ToInt16(chunked.ElementAt(3), 16) / 256);
    }
    public static Sprite Image2Sprite(string path, float PixelPerUnit = 100f)
    {
        /*using (var image = SixLabors.ImageSharp.Image.Load(path))
        {
            Texture2D texture = new(image.width, image.height);
            if (!texture.LoadImage(File.ReadAllBytes(path)))
            {
                throw new Exception("Cannot load File " + path);
            }
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), PixelPerUnit);
        }*/
        Texture2D texture = new(2, 2);
        if (!texture.LoadImage(File.ReadAllBytes(path)))
        {
            throw new Exception("Cannot load File " + path);
        }
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), PixelPerUnit);
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
        Color rgb = new() { 
            r = r,
            g = g,
            b = b
        };
        return rgb;

    }
}