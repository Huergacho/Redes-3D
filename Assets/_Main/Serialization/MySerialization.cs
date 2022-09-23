using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
public static class MySerialization
{
    public static string _path;
    public static string _filename;
    const string _jsonExtension = ".json";
    const string _binExtension = ".bin";
    private static void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    SerializationJSON();
        //}   
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    DeSerializationJSON();
        //}
        
    }
    public static void SerializationJSON<T>(T data, string path, string filename)
    {
        //Asignamos las variables a guardar
        //Asignamos la carpeta
        string gameFolder = Application.dataPath;
        var realPath = Path.Combine(gameFolder, path, filename + _jsonExtension);

        //Creamos el Json
        string json = JsonUtility.ToJson(data,true);

        //Lo escribimos
        StreamWriter file = File.CreateText(realPath);

        //Directory.Exists(path) Existe tal carpeta
        //Directopry.CreateDirectory(path); Crea una carpeta si no existe (Si existe no la crea)
        file.Write(json);
        file.Close();
        //File.WriteAllText(realPath, json); Crea, escribe y cierra.

    }
    public static T DeSerializationJSON<T>(string path, string filename)
    {
        string gameFolder = Application.dataPath;
        var realPath = Path.Combine(gameFolder, path, filename + _jsonExtension);

        StreamReader file = File.OpenText(realPath);
        string json = file.ReadToEnd();
        file.Close();
        //string json = File.ReadAllText(realPath); Abre el archivo, lee y cierra

        var data = JsonUtility.FromJson<T>(json);
        return data;
    }
    public static void SerealizationBin<T>(T data,string path, string filename )
    {

        string gameFolder = Application.dataPath;
        var realPath = Path.Combine(gameFolder, path, filename + _binExtension);

        var file = File.Create(realPath);
        var formatter = new BinaryFormatter();
        formatter.Serialize(file, data);
        file.Close();
    }
    public static T DeserealizationBin<T>(string path, string filename)
    {
        string gameFolder = Application.dataPath;
        var realPath = Path.Combine(gameFolder, path, filename + _binExtension);

        if (!File.Exists(realPath)) return default(T);

        var file = File.OpenRead(realPath);
        var formatter = new BinaryFormatter();
        T data = (T)formatter.Deserialize(file);
        file.Close();

        return data;
    }
}
