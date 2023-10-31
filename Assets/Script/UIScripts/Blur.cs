using UnityEngine;
using System;

public class LinearBlur
{
    private static float _rSum = 0;
    private static float _gSum = 0;
    private static float _bSum = 0;

    private static Texture2D _sourceImage;
    private static int _sourceWidth;
    private static int _sourceHeight;
    private static int _windowSize;

    /// <summary>
    /// another blur function, with down sampling, based on https://forum.unity.com/threads/contribution-texture2d-blur-in-c.185694/#post-2348386
    /// </summary>
    /// <param name="downSample">
    /// down sampling, put 0.5f to get 2x down sample
    /// </param>
    public static Texture2D Blur(Texture2D image, int radius, int iterations, float downSample, FilterMode downSampleMode = FilterMode.Bilinear)
    {
        Texture2D downSampledImage = GPUTextureScaler.Scaled(image, (int)(image.width * downSample), (int)(image.height * downSample), downSampleMode);
        return internalBlur(downSampledImage, radius, iterations);
    }
    /// <param name="downSampleToSize">
    /// down sample to spec size
    /// </param>
    /// <inheritdoc cref="Blur(Texture2D, int, int, float, FilterMode)"/>
    public static Texture2D Blur(Texture2D image, int radius, int iterations, Vector2 downSampleToSize, FilterMode downSampleMode = FilterMode.Bilinear)
    {
        Texture2D downSampledImage = GPUTextureScaler.Scaled(image, (int)downSampleToSize.x, (int)downSampleToSize.y, downSampleMode);
        return internalBlur(downSampledImage, radius, iterations);
    }
    private static Texture2D internalBlur(Texture2D image, int radius, int iterations)
    {
        _windowSize = radius * 2 + 1;
        _sourceWidth = image.width;
        _sourceHeight = image.height;

        var tex = image;

        for (var i = 0; i < iterations; i++)
        {
            tex = OneDimensialBlur(tex, radius, true);
            tex = OneDimensialBlur(tex, radius, false);
        }

        return tex;
    }

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
}