using System;
using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
public class PhysicalSize
{
    private const float mmToInch = 25.4F;
    private readonly static float dpi = (Screen.dpi == 0) ? Resource.DefaultDPI : Screen.dpi;
    public float xMM { get; set; }
    public float yMM { get; set; }
    public float zMM { get; set; }
    public Vector2 ToVector2()
    {
        return new Vector2()
        {
            x = this.xMM * dpi / mmToInch, //i asked wolfram alpha to simiplify the formula idk y but it works
            y = this.yMM * dpi / mmToInch
        };
    }
    public Vector3 ToVector3()
    {
        Vector2 vector2 = ToVector2();
        return new Vector3()
        {
            x = vector2.x,
            y = vector2.y,
            z = this.zMM
        };
    }
    public float ToDistance(bool use2D = false)
    {
        if (use2D)
        {
            return (float)Math.Pow(Math.Pow(xMM, 2) + Math.Pow(yMM, 2), 0.5) * dpi / mmToInch;
        }
        return xMM * dpi / mmToInch;
    }
}
#pragma warning restore IDE1006 // Naming Styles
