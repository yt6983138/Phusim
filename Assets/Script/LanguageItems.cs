using OdinSerializer;
using System;
using System.Collections.Generic;

//[Serializable]
public class LanguageItems : SerializedScriptableObject
{
    public Dictionary<string, string> Language { get; set; }
}
