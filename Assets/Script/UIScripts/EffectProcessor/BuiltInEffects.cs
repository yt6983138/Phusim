using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DownSample : IEffectProcesser
{
    public string Name { get; } = "Down Sample";
    public string Description { get; } = "Down sample texture with multiply (0.5x etc)";

    // parameter start
    public float Multiplier = 1.0f;
    public FilterMode DownSampleMode = FilterMode.Trilinear;
    // parameter end

    public Texture2D ProcessTexture(Texture2D texture)
    {
        return GPUTextureScaler.Scaled(texture, (int)(texture.width * this.Multiplier), (int)(texture.height * this.Multiplier), this.DownSampleMode);
    }
}
/// <summary>
/// Warning: multithreading not implemented!
/// </summary>
public class Tint : IEffectProcesser
{
    public string Name { get; } = "Tint";
    public string Description { get; } = "Tint texture with color as parameter";

    // parameter start
    public Color TintColor = new Color(1, 1, 1, 1);
    public bool MultiThread = false;
    // parameter end

    public Texture2D ProcessTexture(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        if (!this.MultiThread)
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] *= this.TintColor;
            }
            texture.SetPixels(pixels);
            return texture;
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}
public class CPUGaussianBlur : IEffectProcesser // i should try multithreading on this later(its too fucking slow and idk how to use compute shaders)
{
    public string Name { get; } = "Gaussian Blur(CPU)";
    public string Description { get; } = "Gaussian blur using cpu to calculate (can be slow)";

    // parameter start
    public ushort Iterations = 1;
    public int Radius = 1;
    // parameter end

    // private variable start
    private static float _rSum = 0;
    private static float _gSum = 0;
    private static float _bSum = 0;

    private static Texture2D _sourceImage;
    private static int _sourceWidth;
    private static int _sourceHeight;
    private static int _windowSize;
    // private variable end

    // private function start
    private static Texture2D OneDimensialBlur(Texture2D image, int radius, bool horizontal)
    {
        _sourceImage = image;

        var blurred = new Texture2D(image.width, image.height, image.format, false);

        if (horizontal)
        {
            for (int imgY = 0; imgY < _sourceHeight; ++imgY)
            {
                ResetSum();

                for (int imgX = 0; imgX < _sourceWidth; imgX++)
                {
                    if (imgX == 0)
                        for (int x = radius * -1; x <= radius; ++x)
                            AddPixel(GetPixelWithXCheck(x, imgY));
                    else
                    {
                        var toExclude = GetPixelWithXCheck(imgX - radius - 1, imgY);
                        var toInclude = GetPixelWithXCheck(imgX + radius, imgY);

                        SubstPixel(toExclude);
                        AddPixel(toInclude);
                    }

                    blurred.SetPixel(imgX, imgY, CalcPixelFromSum());
                }
            }
        }

        else
        {
            for (int imgX = 0; imgX < _sourceWidth; imgX++)
            {
                ResetSum();

                for (int imgY = 0; imgY < _sourceHeight; ++imgY)
                {
                    if (imgY == 0)
                        for (int y = radius * -1; y <= radius; ++y)
                            AddPixel(GetPixelWithYCheck(imgX, y));
                    else
                    {
                        var toExclude = GetPixelWithYCheck(imgX, imgY - radius - 1);
                        var toInclude = GetPixelWithYCheck(imgX, imgY + radius);

                        SubstPixel(toExclude);
                        AddPixel(toInclude);
                    }

                    blurred.SetPixel(imgX, imgY, CalcPixelFromSum());
                }
            }
        }

        blurred.Apply();
        return blurred;
    }
    private static Color GetPixelWithXCheck(int x, int y)
    {
        if (x <= 0) return _sourceImage.GetPixel(0, y);
        if (x >= _sourceWidth) return _sourceImage.GetPixel(_sourceWidth - 1, y);
        return _sourceImage.GetPixel(x, y);
    }

    private static Color GetPixelWithYCheck(int x, int y)
    {
        if (y <= 0) return _sourceImage.GetPixel(x, 0);
        if (y >= _sourceHeight) return _sourceImage.GetPixel(x, _sourceHeight - 1);
        return _sourceImage.GetPixel(x, y);
    }
    private static void AddPixel(Color pixel)
    {
        _rSum += pixel.r;
        _gSum += pixel.g;
        _bSum += pixel.b;
    }

    private static void SubstPixel(Color pixel)
    {
        _rSum -= pixel.r;
        _gSum -= pixel.g;
        _bSum -= pixel.b;
    }

    private static void ResetSum()
    {
        _rSum = 0.0f;
        _gSum = 0.0f;
        _bSum = 0.0f;
    }

    static Color CalcPixelFromSum()
    {
        return new Color(_rSum / _windowSize, _gSum / _windowSize, _bSum / _windowSize);
    }
    // private function end

    public Texture2D ProcessTexture(Texture2D texture)
    {
        _windowSize = this.Radius * 2 + 1;
        _sourceWidth = texture.width;
        _sourceHeight = texture.height;

        var tex = texture;

        for (var i = 0; i < this.Iterations; i++)
        {
            tex = OneDimensialBlur(tex, this.Radius, true);
            tex = OneDimensialBlur(tex, this.Radius, false);
        }

        return tex;
    }
}

