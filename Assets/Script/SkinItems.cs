//using ProtoBuf;
using OdinSerializer;
using System;
using System.Collections.Generic;

//[ProtoContract]
//[Serializable]
//[Serializable]
public class SkinItems : SerializedScriptableObject
{
    //[ProtoMember(10, DynamicType = true)]
    public Dictionary<string, Dictionary<string, object>> SoundEffect { get; set; }
    //[ProtoMember(15, DynamicType = true)]
    public Dictionary<string, Dictionary<string, object>> Properties { get; set; }
    // comment here
    /*
    public object Clone()
    {
        return new SkinItems
        {
            Textures = this.Textures,
            SoundEffect = this.SoundEffect,
            Properties = this.Properties
        };
    }*/
}
