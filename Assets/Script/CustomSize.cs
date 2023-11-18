using System;
using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
[Serializable]
public class CustomSize
{
    private const float mmToInch = 25.4F;
    private readonly static float dpi = (Screen.dpi == 0) ? Resource.DefaultDPI : Screen.dpi;
    public float xMM { get; set; }
    public float yMM { get; set; }
    public float zMM { get; set; }
    public float PosX
    {
        get
        {
            return xMM * dpi / mmToInch;
        }
        set
        {
            this.xMM = value * mmToInch / dpi;
        }
    }
    public float PosY
    {
        get
        {
            return yMM * dpi / mmToInch;
        }
        set
        {
            this.yMM = value * mmToInch / dpi;
        }
    }
    public float PosZ
    {
        get
        {
            return zMM * dpi / mmToInch;
        }
        set
        {
            this.zMM = value * mmToInch / dpi;
        }
    }
    public Vector2 ToVector2()
    {
        return new Vector2()
        {
            x = this.PosX, //i asked wolfram alpha to simiplify the formula idk y but it works
            y = this.PosY
        };
    }
    public Vector3 ToVector3()
    {
        return new Vector3()
        {
            x = this.PosX,
            y = this.PosY,
            z = this.PosZ
        };
    }
    public float ToDistance(bool use2D = false)
    {
        if (use2D)
        {
            return (float)Math.Pow(Math.Pow(xMM, 2) + Math.Pow(yMM, 2), 0.5) * dpi / mmToInch;
        }
        return xMM * dpi / mmToInch; //wait why did i make this method :thonking:
    }
}
#pragma warning restore IDE1006 // Naming Styles
