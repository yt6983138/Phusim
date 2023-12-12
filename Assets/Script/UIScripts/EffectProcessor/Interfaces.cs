using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// u should have parameter variable to process
/// </summary>
public interface IEffectProcesser
{
    public string Name { get; }
    public string Description { get; }
    public Texture2D ProcessTexture(Texture2D texture);
}
