using System.IO;
using System;
using Newtonsoft.Json;
using System.Text;
using UnityEngine;
using CsvHelper;
using System.Globalization;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using System.Linq;

public static class Serializer
{
    public static JsonSerializerSettings JsonSettings { get; set; }
    public static void SerializeBin<T>(string path, T obj)
    {
        throw new NotImplementedException();
        using (FileStream file = File.Open(path, FileMode.OpenOrCreate))
        {
            //file.Write(SerializationUtility.SerializeValue<T>(obj, DataFormat.Binary));
        }
    }
    public static void SerializeJson<T>(string path, T obj)
    {
        using (FileStream file = File.Open(path, FileMode.OpenOrCreate))
        {
            file.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, JsonSettings)));
        }
    }
    public static void SerializeJson<T>(string path, T obj, JsonSerializerSettings settings)
    {
        using (FileStream file = File.Open(path, FileMode.OpenOrCreate))
        {
            file.Write(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(obj, settings)));
        }
    }
    public static T DeserializeBin<T>(string path)
    {
        throw new NotImplementedException();
        //return SerializationUtility.DeserializeValue<T>(File.ReadAllBytes(path), DataFormat.Binary);
    }
    public static T DeserializeJson<T>(string path)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(path), JsonSettings);
    }
    public static T DeserializeJson<T>(string path, JsonSerializerSettings settings)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(path), settings);
    }
    public static List<T> DeserializeCsv<T>(string path)
    {
        using (var csv = new CsvReader(new StreamReader(path), CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<T>().ToList();
        }
    }
    public static T DeserializeYaml<T>(string path)
    {
        var deserializer = new DeserializerBuilder().Build();
        return deserializer.Deserialize<T>(File.ReadAllText(path));
    }
}