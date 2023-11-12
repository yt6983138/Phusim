using System.IO;
using OdinSerializer;
using Newtonsoft.Json;
using System.Text;
using UnityEngine;
using CsvHelper;
using System.Globalization;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;

public static class Serializer
{
    public static JsonSerializerSettings JsonSettings { get; set; }
    public static void SerializeBin<T>(string path, T obj)
    {
        using (FileStream file = File.Open(path, FileMode.OpenOrCreate))
        {
            file.Write(SerializationUtility.SerializeValue<T>(obj, DataFormat.Binary));
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
        return SerializationUtility.DeserializeValue<T>(File.ReadAllBytes(path), DataFormat.Binary);
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
            return (List<T>)csv.GetRecords<T>();
        }
    }
    public static Dictionary<string, object> DeserializeYamlSimple(string path)
    {
        Dictionary<string, object> keyValuePairs = new();

        foreach (string line in File.ReadAllLines(path))
        {
            try
            {
                string[] pair = line.Split(':', 2);
                string value = pair[1].Trim();
                if (value == "null")
                {
                    keyValuePairs.Add(pair[0].Replace(" ", ""), null);
                }
                else if (value == "true")
                {
                    keyValuePairs.Add(pair[0].Replace(" ", ""), true);
                }
                else if (value == "false")
                {
                    keyValuePairs.Add(pair[0].Replace(" ", ""), false);
                }
                else
                {
                    try
                    {
                        keyValuePairs.Add(pair[0].Replace(" ", ""), float.Parse(value));
                    }
                    catch
                    {
                        keyValuePairs.Add(pair[0].Replace(" ", ""), value);

                    }
                }
            }
            catch { }
        }
        return keyValuePairs;
    }
}