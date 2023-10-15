using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public readonly static Dictionary<string, string> Base64Textures = new()
    {
        { "TapImage", @"" },
        { "TapMultiPressImage", @"" },
        { "DragImage", @"" },
        { "DragMultiPressImage", @"" },
        { "FlickImage", @"" },
        { "FlickMultiPressImage", @"" },
        { "HoldImage", @"" },
        { "HoldMultiPressImage", @"" },
        { "HitEffect", @"" },
        { "Settings", @"" },
        { "Buttons", @"" },
        { "Background", @""}
    };
}