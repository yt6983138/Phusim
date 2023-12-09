using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class ListExtension
{
    public static string ToString<T>(this List<T> list)
    {
        return $"{{ {string.Join(", ", list)} }}";
    }
}

public static class Vector3Extension
{
    public static Vector3 Multiply(this Vector3 first, Vector3 second)
    {
        return new Vector3()
        {
            x = first.x * second.x,
            y = first.y * second.y,
            z = first.z * second.z
        };
    }
}