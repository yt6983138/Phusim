using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Polenter.Serialization;
using System.IO;

public class Serializer
{
    public static SharpSerializerBinarySettings binSettings { get; set; }
    public static JsonSerializerOptions jsonSettings { get; set; }
    public static void SerializeBin(string path, object obj)
    {
        SharpSerializer serializer = new(binSettings);
        serializer.Serialize(obj, path);
    }
    public static void SerializeJson<T>(string path, T obj) // idk y i need to use <t> here
    {
        using (FileStream file = File.Open(path, FileMode.OpenOrCreate))
        {
            file.Write(JsonSerializer.SerializeToUtf8Bytes<T>(obj, jsonSettings));
        }
    }
    public static T DeserializeBin<T>(string path)
    {
        SharpSerializer serializer = new(binSettings);
        return (T)serializer.Deserialize(path);
    }
    public static T DeserializeJson<T>(string path)
    {
        T obj = JsonSerializer.Deserialize<T>(File.ReadAllText(path), jsonSettings);
        return obj;
    }
}