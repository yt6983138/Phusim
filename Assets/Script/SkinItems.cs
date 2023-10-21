//using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//[ProtoContract]
//[Serializable]
[Serializable()]
public class SkinItems
{
    //[ProtoMember(1, DynamicType = true)]
    public Dictionary<string, Dictionary<string, object>> Textures { get; set; }
    //[ProtoMember(10, DynamicType = true)]
    public Dictionary<string, Dictionary<string, object>> SoundEffect { get; set; }
    //[ProtoMember(15, DynamicType = true)]
    public Dictionary<string, Dictionary<string, object>> ButtonProperties { get; set; }
    // comment here
    /*
    public object Clone()
    {
        return new SkinItems
        {
            Textures = this.Textures,
            SoundEffect = this.SoundEffect,
            ButtonProperties = this.ButtonProperties
        };
    }*/
}
